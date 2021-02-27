using System;

namespace Antilatency.UnityPrototypingTools {

    [Serializable]
    public struct HelpBoxDisplay {

        [Serializable]
        public enum IconType {
            None = 0,
            Info = 1,
            Warning = 2,
            Error = 3,
        }

        public IconType Icon { get; private set; }

        public string Message { get; private set; }

        public bool IsVisible {
            get { return Message != null; }
        }

        public void ShowError(string str) {
            ShowMessage(str, IconType.Error);
        }

        public void ShowWarning(string str) {
            ShowMessage(str, IconType.Warning);
        }

        public void ShowInfo(string str) {
            ShowMessage(str, IconType.Info);
        }

        public void ShowMessage(string str, IconType iconType) {
            Icon = iconType;
            Message = str;
        }

        public void Hide() {
            Icon = IconType.None;
            Message = null;
        }
    }
}
