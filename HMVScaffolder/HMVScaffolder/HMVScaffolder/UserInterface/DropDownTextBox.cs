using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Media;

namespace HMVScaffolder.Mvc
{
	public class DropDownTextBox : TextBox
	{
		private ComboBox _parent;

		public DropDownTextBox()
		{
		}

		protected override AutomationPeer OnCreateAutomationPeer()
		{
			if (this._parent == null)
			{
				DependencyObject parent = this;
				while (parent == null || !(parent is ComboBox))
				{
					parent = VisualTreeHelper.GetParent(parent);
				}
				this._parent = (ComboBox)parent;
			}
			return new DropDownTextBox.DropDownTextBoxAutomationPeer(this);
		}

		private class DropDownTextBoxAutomationPeer : TextBoxAutomationPeer, IExpandCollapseProvider
		{
			private DropDownTextBox _dropDownTextBox;

			public ExpandCollapseState ExpandCollapseState
			{
				get
				{
					if (!this._dropDownTextBox._parent.IsDropDownOpen)
					{
						return ExpandCollapseState.Collapsed;
					}
					return ExpandCollapseState.Expanded;
				}
			}

			public DropDownTextBoxAutomationPeer(DropDownTextBox dropDownTextBox) : base(dropDownTextBox)
			{
				this._dropDownTextBox = dropDownTextBox;
			}

			public void Collapse()
			{
				this._dropDownTextBox._parent.IsDropDownOpen = false;
			}

			public void Expand()
			{
				this._dropDownTextBox._parent.IsDropDownOpen = true;
			}

			protected override AutomationControlType GetAutomationControlTypeCore()
			{
				return AutomationControlType.ComboBox;
			}

			public override object GetPattern(PatternInterface patternInterface)
			{
				if (patternInterface == PatternInterface.ExpandCollapse)
				{
					return this;
				}
				return base.GetPattern(patternInterface);
			}
		}
	}
}