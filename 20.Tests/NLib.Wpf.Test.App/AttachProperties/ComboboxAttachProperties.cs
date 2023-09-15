#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

#endregion

namespace NLib.Wpf.Controls.Utils
{
    #region Combobox AttachProperties

    /// <summary>
    /// The Combobox AttachProperties class.
    /// </summary>
    public class ComboboxAttachProperties : DependencyObject
    {
        #region IsFilterOnAutoCompleteEnabled attached property

        public static readonly DependencyProperty IsFilterOnAutocompleteEnabledProperty =
          DependencyProperty.RegisterAttached(
            "IsFilterOnAutocompleteEnabled",
            typeof(bool),
            typeof(ComboBox),
            new PropertyMetadata(default(bool), OnIsFilterOnAutocompleteEnabledChanged));

        public static void SetIsFilterOnAutocompleteEnabled(DependencyObject attachingElement, bool value) =>
          attachingElement.SetValue(ComboboxAttachProperties.IsFilterOnAutocompleteEnabledProperty, value);

        public static bool GetIsFilterOnAutocompleteEnabled(DependencyObject attachingElement) =>
          (bool)attachingElement.GetValue(IsFilterOnAutocompleteEnabledProperty);

        #endregion

        #region Static Variables

        // Use hash tables for faster lookup
        private static Dictionary<TextBox, ComboBox> TextBoxComboBoxMap { get; }
        private static Dictionary<TextBox, int> TextBoxSelectionStartMap { get; }
        private static Dictionary<ComboBox, TextBox> ComboBoxTextBoxMap { get; }
        private static bool IsNavigationKeyPressed { get; set; }

        #endregion

        #region Constructor (static)

        /// <summary>
        /// Constructor.
        /// </summary>
        static ComboboxAttachProperties()
        {
            TextBoxComboBoxMap = new Dictionary<TextBox, ComboBox>();
            TextBoxSelectionStartMap = new Dictionary<TextBox, int>();
            ComboBoxTextBoxMap = new Dictionary<ComboBox, TextBox>();
        }

        #endregion

        #region Private Methods (static)

        private static void OnIsFilterOnAutocompleteEnabledChanged(DependencyObject attachingElement,
          DependencyPropertyChangedEventArgs e)
        {
            if (!(attachingElement is System.Windows.Controls.ComboBox comboBox
              && comboBox.IsEditable))
            {
                return;
            }

            if (!(bool)e.NewValue)
            {
                DisableAutocompleteFilter(comboBox);
                return;
            }

            if (!comboBox.IsLoaded)
            {
                comboBox.Loaded += EnableAutocompleteFilterOnComboBoxLoaded;
                return;
            }
            EnableAutocompleteFilter(comboBox);
        }

        private static async void FilterOnTextInput(object sender, TextChangedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
              {
                  if (IsNavigationKeyPressed)
                  {
                      return;
                  }

                  var textBox = sender as TextBox;
                  int textBoxSelectionStart = textBox.SelectionStart;
                  TextBoxSelectionStartMap[textBox] = textBoxSelectionStart;

                  string changedTextOnAutocomplete = textBox.Text.Substring(0, textBoxSelectionStart);
                  if (TextBoxComboBoxMap.TryGetValue(textBox, out ComboBox comboBox))
                  {
                      comboBox.Items.Filter = item => item.ToString().StartsWith(
                changedTextOnAutocomplete,
                StringComparison.OrdinalIgnoreCase);
                  }
              },
              DispatcherPriority.Background);
        }

        private static async void HandleKeyDownWhileFiltering(object sender, KeyEventArgs e)
        {
            var comboBox = sender as System.Windows.Controls.ComboBox;
            if (!ComboBoxTextBoxMap.TryGetValue(comboBox, out TextBox textBox))
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Down
                  when comboBox.Items.CurrentPosition < comboBox.Items.Count - 1
                       && comboBox.Items.MoveCurrentToNext():
                case Key.Up
                  when comboBox.Items.CurrentPosition > 0
                       && comboBox.Items.MoveCurrentToPrevious():
                    {
                        // Prevent the filter from re-apply as this would override the
                        // current selection start index
                        IsNavigationKeyPressed = true;

                        // Ensure the Dispatcher en-queued delegate 
                        // (and the invocation of the SelectCurrentItem() method)
                        // executes AFTER the FilterOnTextInput() event handler.
                        // This is because key input events have a higher priority
                        // than text change events by default. The goal is to make the filtering 
                        // triggered by the TextBox.TextChanged event ignore the changes 
                        // introduced by this KeyDown event.
                        // DispatcherPriority.ContextIdle will force to "override" this behavior.
                        await Application.Current.Dispatcher.InvokeAsync(() =>
                          {
                              SelectCurrentItem(textBox, comboBox);
                              IsNavigationKeyPressed = false;
                          },
                          DispatcherPriority.ContextIdle);

                        break;
                    }
            }
        }

        private static void SelectCurrentItem(TextBox textBox, System.Windows.Controls.ComboBox comboBox)
        {
            comboBox.SelectedItem = comboBox.Items.CurrentItem;
            if (TextBoxSelectionStartMap.TryGetValue(textBox, out int selectionStart))
            {
                textBox.SelectionStart = selectionStart;
            }
        }

        private static void EnableAutocompleteFilterOnComboBoxLoaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as System.Windows.Controls.ComboBox;
            EnableAutocompleteFilter(comboBox);
        }

        private static void EnableAutocompleteFilter(System.Windows.Controls.ComboBox comboBox)
        {
            if (comboBox.TryFindVisualChildElement(out TextBox editTextBox))
            {
                TextBoxComboBoxMap.Add(editTextBox, comboBox);
                ComboBoxTextBoxMap.Add(comboBox, editTextBox);
                editTextBox.TextChanged += FilterOnTextInput;

                // Need to receive handled KeyDown event
                comboBox.AddHandler(UIElement.PreviewKeyDownEvent, new KeyEventHandler(HandleKeyDownWhileFiltering), true);
            }
        }

        private static void DisableAutocompleteFilter(System.Windows.Controls.ComboBox comboBox)
        {
            if (comboBox.TryFindVisualChildElement(out TextBox editTextBox))
            {
                TextBoxComboBoxMap.Remove(editTextBox);
                editTextBox.TextChanged -= FilterOnTextInput;
            }
        }

        #endregion
    }

    #endregion

    #region ComboboxAttachProperties ExtensionMethods

    /// <summary>
    /// The ComboboxAttachProperties ExtensionMethods class.
    /// </summary>
    public static class ComboboxAttachPropertiesExtensionMethods
    {
        #region TryFindVisualChildElement

        /// <summary>
        /// Traverses the visual tree towards the leafs until an element with a matching element type is found.
        /// </summary>
        /// <typeparam name="TChild">The type the visual child must match.</typeparam>
        /// <param name="parent"></param>
        /// <param name="resultElement"></param>
        /// <returns></returns>
        public static bool TryFindVisualChildElement<TChild>(this DependencyObject parent,
            out TChild resultElement)
          where TChild : DependencyObject
        {
            resultElement = null;

            if (parent is Popup popup)
            {
                parent = popup.Child;
                if (parent == null)
                {
                    return false;
                }
            }

            for (var childIndex = 0; childIndex < VisualTreeHelper.GetChildrenCount(parent); childIndex++)
            {
                DependencyObject childElement = VisualTreeHelper.GetChild(parent, childIndex);
                if (childElement is TChild child)
                {
                    resultElement = child;
                    return true;
                }

                if (childElement.TryFindVisualChildElement(out resultElement))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }

    #endregion
}
