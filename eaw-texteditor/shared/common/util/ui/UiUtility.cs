using System.Windows;
using System.Windows.Media;

namespace eaw_texteditor.shared.common.util.ui
{
    internal static class UiUtility
    {
        public static T TryFindParent<T>(this DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = GetParentObject(child);
            if (parentObject == null) return null;
            T parent = parentObject as T;
            return parent ?? TryFindParent<T>(parentObject);
        }
        
        public static DependencyObject GetParentObject(this DependencyObject child)
        {
            switch (child)
            {
                case null:
                    return null;
                case ContentElement contentElement:
                {
                    DependencyObject parent = ContentOperations.GetParent(contentElement);
                    if (parent != null) return parent;

                    return contentElement is FrameworkContentElement fce ? fce.Parent : null;
                }
                case FrameworkElement frameworkElement:
                {
                    DependencyObject parent = frameworkElement.Parent;
                    if (parent != null) return parent;
                    break;
                }
            }

            return VisualTreeHelper.GetParent(child);
        }
    }
}
