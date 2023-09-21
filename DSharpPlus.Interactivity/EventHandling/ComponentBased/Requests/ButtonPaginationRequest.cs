using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Enums;
namespace DSharpPlus.Interactivity.EventHandling
{
    internal class ButtonPaginationRequest : IPaginationRequest
    {
        private int _index;
        private readonly List<Page> _pages = new();

        private readonly TaskCompletionSource<bool> _tcs = new();

        private readonly CancellationToken _token;
        private readonly DiscordUser _user;
        private readonly DiscordMessage _message;
        private readonly PaginationButtons _buttons;
        private readonly PaginationBehaviour _wrapBehavior;
        private readonly ButtonPaginationBehavior _behaviorBehavior;

        public ButtonPaginationRequest(DiscordMessage message, DiscordUser user,
            PaginationBehaviour behavior, ButtonPaginationBehavior behaviorBehavior,
            PaginationButtons buttons, IEnumerable<Page> pages, CancellationToken token)
        {
            this._user = user;
            this._token = token;
            this._buttons = new(buttons);
            this._message = message;
            this._wrapBehavior = behavior;
            this._behaviorBehavior = behaviorBehavior;
            this._pages.AddRange(pages);

            this._token.Register(() => this._tcs.TrySetResult(false));
        }

        public int PageCount => this._pages.Count;

        public Task<Page> GetPageAsync()
        {
            var page = Task.FromResult(this._pages[this._index]);

            if (this.PageCount is 1)
            {
                this._buttons.SkipLeft.Disable();
                this._buttons.Left.Disable();
                this._buttons.Right.Disable();
                this._buttons.SkipRight.Disable();

                this._buttons.Stop.Enable();
                return page;
            }

            if (this._wrapBehavior is PaginationBehaviour.WrapAround)
                return page;

            this._buttons.SkipLeft.Disabled = this._index < 2;

            this._buttons.Left.Disabled = this._index < 1;

            this._buttons.Right.Disabled = this._index >= this.PageCount - 1;

            this._buttons.SkipRight.Disabled = this._index >= this.PageCount - 2;

            return page;
        }

        public Task SkipLeftAsync()
        {
            if (this._wrapBehavior is PaginationBehaviour.WrapAround)
            {
                this._index = this._index is 0 ? this._pages.Count - 1 : 0;
                return Task.CompletedTask;
            }

            this._index = 0;

            return Task.CompletedTask;
        }

        public Task SkipRightAsync()
        {
            if (this._wrapBehavior is PaginationBehaviour.WrapAround)
            {
                this._index = this._index == this.PageCount - 1 ? 0 : this.PageCount - 1;
                return Task.CompletedTask;
            }

            this._index = this._pages.Count - 1;

            return Task.CompletedTask;
        }

        public Task NextPageAsync()
        {
            this._index++;

            if (this._wrapBehavior is PaginationBehaviour.WrapAround)
            {
                if (this._index >= this.PageCount)
                    this._index = 0;

                return Task.CompletedTask;
            }

            this._index = Math.Min(this._index, this.PageCount - 1);

            return Task.CompletedTask;
        }

        public Task PreviousPageAsync()
        {
            this._index--;

            if (this._wrapBehavior is PaginationBehaviour.WrapAround)
            {
                if (this._index is - 1)
                    this._index = this._pages.Count - 1;

                return Task.CompletedTask;
            }

            this._index = Math.Max(this._index, 0);

            return Task.CompletedTask;
        }

        public Task<PaginationEmojis> GetEmojisAsync()
            => Task.FromException<PaginationEmojis>(new NotSupportedException("Emojis aren't supported for this request."));

        public Task<IEnumerable<DiscordButtonComponent>> GetButtonsAsync()
            => Task.FromResult((IEnumerable<DiscordButtonComponent>)this._buttons.ButtonArray);

        public Task<DiscordMessage> GetMessageAsync() => Task.FromResult(this._message);

        public Task<DiscordUser> GetUserAsync() => Task.FromResult(this._user);

        public Task<TaskCompletionSource<bool>> GetTaskCompletionSourceAsync() => Task.FromResult(this._tcs);

        // This is essentially the stop method. //
        public async Task DoCleanupAsync()
        {
            switch (this._behaviorBehavior)
            {
                case ButtonPaginationBehavior.Disable:
                    var buttons = this._buttons.ButtonArray.Select(b => b.Disable());

                    var builder = new DiscordMessageBuilder()
                        .WithContent(this._pages[this._index].Content)
                        .AddEmbed(this._pages[this._index].Embed)
                        .AddComponents(buttons);

                    await builder.ModifyAsync(this._message);
                    break;

                case ButtonPaginationBehavior.DeleteButtons:
                    builder = new DiscordMessageBuilder()
                        .WithContent(this._pages[this._index].Content)
                        .AddEmbed(this._pages[this._index].Embed);

                    await builder.ModifyAsync(this._message);
                    break;

                case ButtonPaginationBehavior.DeleteMessage:
                    await this._message.DeleteAsync();
                    break;

                case ButtonPaginationBehavior.Ignore:
                    break;
            }
        }
    }
}
