// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InteractiveLayer.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models
{
    #region

    using Microsoft.JSInterop;

    using NPComplet.YourDeals.Controls.OpenStreetMaps.Models.Events;

    #endregion

    /// <summary>
    /// The interactive layer.
    /// </summary>
    public abstract class InteractiveLayer : Layer
    {
        /// <summary>
        /// The mouse event handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public delegate void MouseEventHandler(InteractiveLayer sender, MouseEvent e);

        /// <summary>
        /// The on click.
        /// </summary>
        public event MouseEventHandler OnClick;

        /// <summary>
        /// The on context menu.
        /// </summary>
        public event MouseEventHandler OnContextMenu;

        /// <summary>
        /// The on dbl click.
        /// </summary>
        public event MouseEventHandler OnDblClick;

        /// <summary>
        /// The on mouse down.
        /// </summary>
        public event MouseEventHandler OnMouseDown;

        /// <summary>
        /// The on mouse out.
        /// </summary>
        public event MouseEventHandler OnMouseOut;

        /// <summary>
        /// The on mouse over.
        /// </summary>
        public event MouseEventHandler OnMouseOver;

        /// <summary>
        /// The on mouse up.
        /// </summary>
        public event MouseEventHandler OnMouseUp;

        /// <summary>
        ///     When true, a mouse event on this layer will trigger the same event on the map (unless L.DomEvent.stopPropagation is
        ///     used).
        /// </summary>
        public virtual bool IsBubblingMouseEvents { get; set; } = true;

        /// <summary>
        ///     If false, the layer will not emit mouse events and will act as a part of the underlying map. (events currently not
        ///     implemented in BlazorLeaflet)
        /// </summary>
        public bool IsInteractive { get; set; } = true;

        /// <summary>
        /// The notify click.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyClick(MouseEvent eventArgs)
        {
            this.OnClick?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify context menu.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyContextMenu(MouseEvent eventArgs)
        {
            this.OnContextMenu?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify dbl click.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyDblClick(MouseEvent eventArgs)
        {
            this.OnDblClick?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify mouse down.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyMouseDown(MouseEvent eventArgs)
        {
            this.OnMouseDown?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify mouse out.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyMouseOut(MouseEvent eventArgs)
        {
            this.OnMouseOut?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify mouse over.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyMouseOver(MouseEvent eventArgs)
        {
            this.OnMouseOver?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify mouse up.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyMouseUp(MouseEvent eventArgs)
        {
            this.OnMouseUp?.Invoke(this, eventArgs);
        }
    }
}