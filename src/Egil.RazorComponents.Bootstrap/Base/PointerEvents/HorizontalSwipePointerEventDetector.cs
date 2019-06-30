using System;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Base.PointerEvents
{
    public class HorizontalSwipePointerEventDetector
    {
        private const string PointerTypeMouse = "mouse";

        private readonly Action _swipeLeftAction;
        private readonly Action _swipeRightAction;

        private bool _tracking = false;
        private long? _deviceId = null;
        private long? _startX = null;

        public long DeltaBeforeTriggering { get; set; } = 5;

        public bool IgnoreMouseEvents { get; set; } = true;

        public HorizontalSwipePointerEventDetector(Action swipeRightAction, Action swipeLeftAction)
        {
            _swipeRightAction = swipeRightAction;
            _swipeLeftAction = swipeLeftAction;
        }
        
        internal void OnPointerDownHandler(UIPointerEventArgs e)
        {
            if (ShouldSkip(e)) return;

            _tracking = true;
            _deviceId = e.PointerId;
            _startX = e.ClientX;
        }

        internal void OnPointerMoveHandler(UIPointerEventArgs e)
        {
            if (ShouldSkip(e)) return;
            if (!_tracking || _deviceId != e.PointerId || !_startX.HasValue) return;

            var diffX = _startX.Value - e.ClientX;

            if (Math.Abs(diffX) < DeltaBeforeTriggering) return;
            if (diffX > 0)

                _swipeLeftAction();
            else
                _swipeRightAction();
        }

        internal void OnPointerUpHandler(UIPointerEventArgs e)
        {
            if (ShouldSkip(e)) return;
            if (!_tracking || _deviceId != e.PointerId) return;

            _tracking = false;
            _deviceId = null;
            _startX = null;
        }

        private bool ShouldSkip(UIPointerEventArgs e)
        {
            return IgnoreMouseEvents && e.PointerType.Equals(PointerTypeMouse, StringComparison.OrdinalIgnoreCase);
        }
    }
}
