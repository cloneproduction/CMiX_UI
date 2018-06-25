using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CMiX.Resources
{
    public static class ComboBoxBehavior
    {

        public static object GetSelectedItem(DependencyObject obj)
        {
            return (object)obj.GetValue(SelectedItemProperty);
        }

        public static void SetSelectedItem(DependencyObject obj, object value)
        {
            obj.SetValue(SelectedItemProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedIte.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.RegisterAttached("SelectedItem", typeof(object), typeof(ComboBoxBehavior), new UIPropertyMetadata(null, SelectedItemChanged));


        private static List<WeakReference> ComboBoxes = new List<WeakReference>();
        private static void SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)d;

            // Set the selected item of the ComboBox since the value changed
            if (cb.SelectedItem != e.NewValue) cb.SelectedItem = e.NewValue;

            // If we already handled this ComboBox - return
            if (ComboBoxes.SingleOrDefault(o => o.Target == cb) != null) return;

            // Check if the ItemsSource supports notifications
            if (cb.ItemsSource is INotifyCollectionChanged)
            {
                // Add ComboBox to the list of handled combo boxes so we do not handle it again in the future
                ComboBoxes.Add(new WeakReference(cb));

                // When the ItemsSource collection changes we set the SelectedItem to correct value (using Equals)
                ((INotifyCollectionChanged)cb.ItemsSource).CollectionChanged +=
                    delegate (object sender, NotifyCollectionChangedEventArgs e2)
                    {
                        var collection = (IEnumerable<object>)sender;
                        cb.SelectedItem = collection.SingleOrDefault(o => o.Equals(GetSelectedItem(cb)));
                    };

                // If the user has selected some new value in the combo box - update the attached property too
                cb.SelectionChanged += delegate (object sender, SelectionChangedEventArgs e3)
                {
                    // We only want to handle cases that actually change the selection
                    if (e3.AddedItems.Count == 1)
                    {
                        SetSelectedItem((DependencyObject)sender, e3.AddedItems[0]);
                    }
                };
            }
        }
    }
}