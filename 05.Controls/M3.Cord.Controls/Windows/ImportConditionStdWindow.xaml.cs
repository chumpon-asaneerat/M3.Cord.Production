#region Using

using M3.Cord.Models;
using NLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for ImportConditionStdWindow.xaml
    /// </summary>
    public partial class ImportConditionStdWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ImportConditionStdWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private ExcelModel model = new ExcelModel();
        private bool onImporting = false;

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            model.SheetItemChanges += Model_SheetItemChanges;
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            model.SheetItemChanges -= Model_SheetItemChanges;
        }

        #endregion

        #region Window Closing

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = onImporting;
        }

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdImport_Click(object sender, RoutedEventArgs e)
        {
            if (Imports())
            {
                DialogResult = true;
            }
        }

        private void cmdChooseExcel_Click(object sender, RoutedEventArgs e)
        {
            ChooseExcelFile();
        }

        #endregion

        #region ExcelModel Handlers

        private void Model_SheetItemChanges(object sender, ExcelWorksheetArgs evt)
        {
            if (null != evt && null != evt.Sheet)
            {
                var sheet = evt.Sheet;
                lvMapPreview.Setup<ConditionStd>(sheet);
            }
        }

        #endregion

        #region Private Methods

        private void ChooseExcelFile()
        {
            if (model.Open())
            {
                wsMap.Setup<ConditionStd>(model);
            }

            txtFileName.Text = model.FileName;
        }

        private void EanbleButtons(bool enable)
        {
            cmdCancel.IsEnabled = enable;
            cmdImport.IsEnabled = enable;
            cmdChooseExcel.IsEnabled = enable;
        }

        private bool Imports()
        {
            var items = lvMapPreview.Items;
            if (null == items || items.Count <= 0)
            {
                var mbox = M3CordApp.Windows.MessageBox;
                mbox.Owner = this;
                string msg = "Plese Click 'Read' button before Import.";
                mbox.Setup(msg);
                mbox.ShowDialog();
                return false; // No items
            }

            onImporting = true;
            EanbleButtons(false); // while import disable all buttons.

            var errors = new List<ImportError>();

            var prog = M3CordApp.Windows.ProgressDialog;
            prog.Owner = this;
            prog.Setup(items.Count);
            prog.Show();

            int iCnt = 2; // excel first row is column name.
            foreach (var item in items)
            {
                var ret = ConditionStd.Save(item as ConditionStd);
                if (ret.HasError)
                {
                    // get debug string.
                    string dataString = item.DebugString();
                    errors.Add(new ImportError()
                    {
                        RowNo = iCnt,
                        ErrMsg = ret.ErrMsg,
                        DataString = dataString
                    });
                }
                prog.Increment();

                iCnt++;
            }
            // Close progress dialog.
            prog.Close();

            if (null != errors && errors.Count > 0)
            {
                /*
                var errWin = M3CordApp.Windows.ImportReport;
                errWin.Owner = this;
                errWin.Setup(errors);
                errWin.ShowDialog();
                */
            }

            EanbleButtons(true); // completed import enable all buttons.
            onImporting = false;

            return true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup()
        {

        }

        #endregion
    }
}
