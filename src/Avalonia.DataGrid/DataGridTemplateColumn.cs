using System;
using Avalonia.Controls.Presenters;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.Templates;

namespace Avalonia.Controls
{
    /// <summary>
    /// Represents a <see cref="T:Avalonia.Controls.DataGrid" /> column that hosts generic templated content in its cells.
    /// </summary>
    public class DataGridTemplateColumn : DataGridBoundColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Avalonia.Controls.DataGridTemplateColumn" /> class.
        /// </summary>
        public DataGridTemplateColumn()
        {
            BindingTarget = ContentPresenter.ContentProperty;
        }

        #region Properties

        public static readonly AvaloniaProperty<DataTemplate> CellTemplateProperty
            = AvaloniaProperty.Register<DataGridTemplateColumn, DataTemplate>("CellTemplate", inherits: false);

        /// <summary>
        /// Template which will host our cell view
        /// </summary>
        public DataTemplate CellTemplate
        {
            get { return GetValue(CellTemplateProperty); }
            set { SetValue(CellTemplateProperty, value); }
        }

        #endregion

        #region View Generation

        /// <summary>
        /// Creates the Content Control which will host our template in cell
        /// </summary>
        /// <param name="cell">The cell that will receive the view</param>
        private Control LoadTemplateContent(DataGridCell cell)
        {
            if (CellTemplate != null)
            {
                ContentPresenter contentPresenter = new ContentPresenter();

                contentPresenter.Bind(ContentPresenter.ContentProperty, new Binding());
                contentPresenter.ContentTemplate = CellTemplate;
                return contentPresenter;
            }

            return null;
        }

        protected override Control GenerateElement(DataGridCell cell, object dataItem)
        {
            return LoadTemplateContent(cell);
        }

        protected override object PrepareCellForEdit(Control editingElement, RoutedEventArgs editingEventArgs)
        {
            return null;
        }

        protected override Control GenerateEditingElementDirect(DataGridCell cell, object dataItem)
        {
            return LoadTemplateContent(cell);
        }

        #endregion

        #region Property Changed

        protected internal override void RefreshCellContent(Control element, string propertyName)
        {
            DataGridCell cell = element as DataGridCell;
            if (cell != null)
            {
                if ((string.Compare(propertyName, "CellTemplate", StringComparison.Ordinal) == 0))
                {
                    GenerateElement(cell, null);
                    return;
                }
            }

            base.RefreshCellContent(element, propertyName);
        }

        #endregion
    }
}