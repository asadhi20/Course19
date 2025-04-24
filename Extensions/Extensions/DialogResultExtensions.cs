using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelperClasses.Extensions
{
    public static class DialogResultExtensions
    {
        #region Information Dialog
        public static DialogResult ShowMessageBoxInfoDial(this DialogResult dialogResult, string message, string title = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, MessageBoxOptions options = (MessageBoxOptions)0, bool displayHelpButton = false) =>
            MessageBox.Show(text: message, caption: title, buttons: buttons, icon: MessageBoxIcon.Information, defaultButton: defaultButton, options: options, displayHelpButton: displayHelpButton);


        // IWin32Window
        public static DialogResult ShowMessageBoxInfoDial(this DialogResult dialogResult, IWin32Window owner, string message, string title = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, MessageBoxOptions options = (MessageBoxOptions)0) =>
            MessageBox.Show(owner: owner, text: message, caption: title, buttons: buttons, icon: MessageBoxIcon.Information, defaultButton: defaultButton, options: options);
        #endregion

        #region Error Dialog
        public static DialogResult ShowMessageBoxErrorDial(this DialogResult dialogResult, string message, string title = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, MessageBoxOptions options = (MessageBoxOptions)0, bool displayHelpButton = false) =>
            MessageBox.Show(text: message, caption: title, buttons: buttons, icon: MessageBoxIcon.Error, defaultButton: defaultButton, options: options, displayHelpButton: displayHelpButton);

        // IWin32Window
        public static DialogResult ShowMessageBoxErrorDial(this DialogResult dialogResult, IWin32Window owner, string message, string title = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, MessageBoxOptions options = (MessageBoxOptions)0) =>
            MessageBox.Show(owner: owner, text: message, caption: title, buttons: buttons, icon: MessageBoxIcon.Error, defaultButton: defaultButton, options: options);
        #endregion

        #region Question Dialog
        public static DialogResult ShowMessageBoxQuestionDial(this DialogResult dialogResult, string message, string title = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, MessageBoxOptions options = (MessageBoxOptions)0, bool displayHelpButton = false) =>
            MessageBox.Show(text: message, caption: title, buttons: buttons, icon: MessageBoxIcon.Question, defaultButton: defaultButton, options: options, displayHelpButton: displayHelpButton);

        // IWin32Window
        public static DialogResult ShowMessageBoxQuestionDial(this DialogResult dialogResult, IWin32Window owner, string message, string title = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, MessageBoxOptions options = (MessageBoxOptions)0) =>
            MessageBox.Show(owner: owner, text: message, caption: title, buttons: buttons, icon: MessageBoxIcon.Question, defaultButton: defaultButton, options: options);
        #endregion

        #region Warning Dialog
        public static DialogResult ShowMessageBoxWarningDial(this DialogResult dialogResult, string message, string title = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, MessageBoxOptions options = (MessageBoxOptions)0, bool displayHelpButton = false) =>
            MessageBox.Show(text: message, caption: title, buttons: buttons, icon: MessageBoxIcon.Warning, defaultButton: defaultButton, options: options, displayHelpButton: displayHelpButton);

        // IWin32Window
        public static DialogResult ShowMessageBoxWarningDial(this DialogResult dialogResult, IWin32Window owner, string message, string title = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, MessageBoxOptions options = (MessageBoxOptions)0) =>
            MessageBox.Show(owner: owner, text: message, caption: title, buttons: buttons, icon: MessageBoxIcon.Warning, defaultButton: defaultButton, options: options);
        #endregion
    }
}
