#region Using

using M3.Cord.Models;
using M3.Cord.Windows;
using NLib.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

//using NLib.Services;
//using M3.Cord.Models;
//using M3.Services;

#endregion

namespace M3.Cord
{
    public class M3CordApp
    {
        /// <summary>
        /// Current.
        /// </summary>
        public static class Current
        {
            /// <summary>
            /// Gets Current User.
            /// </summary>
            public static UserInfo User { get { return SignInManager.Instance.User; } }
        }

        /// <summary>
        /// Pages Static class.
        /// </summary>
        public static class Pages
        {
            #region Static Methods

            private static Dictionary<Type, UserControl> _pages = new Dictionary<Type, UserControl>();

            private static T GetPage<T>()
                where T : UserControl, new()
            {
                Type type = typeof(T);
                if (!_pages.ContainsKey(type)) 
                {
                    lock (typeof(M3CordApp))
                    {
                        T inst = new T();
                        _pages.Add(type, inst);
                    }
                }

                T ret = _pages[type] as T;

                return ret;
            }

            #endregion

            #region Common Method(s)

            /// <summary>
            /// Goto Cord Main Menu.
            /// </summary>
            public static void GotoCordMainMenu()
            {
                var page = Pages.M3CordMainMenu;
                page.Setup();
                PageContentManager.Instance.Current = page;
            }
            /// <summary>
            /// Goto Cord Master Menu.
            /// </summary>
            public static void GotoCordMasterMenu()
            {
                var page = Pages.MasterDataMenu;
                PageContentManager.Instance.Current = page;
            }
            /// <summary>
            /// Goto DIP MC Menu.
            /// </summary>
            public static void GotoDIPMCMenu()
            {
                var page = Pages.DIPMC;
                page.Setup();
                PageContentManager.Instance.Current = page;
            }
            /// <summary>
            /// Goto Production Report Menu.
            /// </summary>
            public static void GotoProductionReportMenu()
            {
                var page = Pages.ProductionReport;
                page.Setup();
                PageContentManager.Instance.Current = page;
            }
            /// <summary>
            /// Goto Solution Menu.
            /// </summary>
            public static void GotoSolutionMenu()
            {
                var page = Pages.SolutionMenu;
                page.Setup();
                PageContentManager.Instance.Current = page;
            }
            /// <summary>
            /// Goto DIP Operation Menu.
            /// </summary>
            public static void GotoDIPOperationMenu(DIPMC mc)
            {
                var page = Pages.DIPOperation;
                page.Setup(mc);
                PageContentManager.Instance.Current = page;
            }
            /// <summary>
            /// Goto Ageing Operation Menu.
            /// </summary>
            public static void GotoAgeingOperationMenu()
            {
                var page = Pages.AgeingOperation;
                page.Setup();
                PageContentManager.Instance.Current = page;
            }

            #endregion

            #region Main Menu (Cord)

            /// <summary>Gets M3 Cord MainMenu Page.</summary>
            public static Cord.Pages.M3CordMainMenuPage M3CordMainMenu
            {
                get 
                { 
                    return GetPage<Cord.Pages.M3CordMainMenuPage>(); 
                }
            }

            #endregion

            #region Master Data Main Menu

            /// <summary>Gets M3 Cord Master Data Menu Page.</summary>
            public static Cord.Pages.MasterDataManuPage MasterDataMenu
            {
                get { return GetPage<Cord.Pages.MasterDataManuPage>(); }
            }

            #endregion

            #region Production Report Main Menu

            /// <summary>Gets M3 Cord Production Report Menu Page.</summary>
            public static Cord.Pages.ProductionReportMenu ProductionReport
            {
                get { return GetPage<Cord.Pages.ProductionReportMenu>(); }
            }

            #endregion

            #region Solution Main Menu

            /// <summary>Gets M3 CordSolution Menu Page.</summary>
            public static Cord.Pages.SolutionMenuPage SolutionMenu
            {
                get { return GetPage<Cord.Pages.SolutionMenuPage>(); }
            }

            #endregion

            #region QA Main Menu

            /// <summary>Gets QA Menu Page.</summary>
            public static Cord.Pages.QAMainMenuPage QAMenu
            {
                get { return GetPage<Cord.Pages.QAMainMenuPage>(); }
            }

            #endregion

            #region Master Data (comment out)
            /*
            /// <summary>Gets M3 Cord Product Item Code Manage Page.</summary>
            public static Cord.Pages.ProductItemCodeManagePage ProductItemCodeManage
            {
                get { return GetPage<Cord.Pages.ProductItemCodeManagePage>(); }
            }
            /// <summary>Gets M3 Cord Customer Manage Page.</summary>
            public static Cord.Pages.CustomerManagePage CustomerManage
            {
                get { return GetPage<Cord.Pages.CustomerManagePage>(); }
            }
            */
            #endregion

            #region Condition Standard

            /// <summary>Gets S1 Condition Manage Page.</summary>
            public static Cord.Pages.S1StdManagePage S1StdManage
            {
                get { return GetPage<Cord.Pages.S1StdManagePage>(); }
            }
            /// <summary>Gets S4x1 Condition Manage Page.</summary>
            public static Cord.Pages.S4x1StdManagePage S4x1StdManage
            {
                get { return GetPage<Cord.Pages.S4x1StdManagePage>(); }
            }
            /// <summary>Gets S4x2 Condition Manage Page.</summary>
            public static Cord.Pages.S4x2StdManagePage S4x2StdManage
            {
                get { return GetPage<Cord.Pages.S4x2StdManagePage>(); }
            }
            /// <summary>Gets S5 Condition Manage Page.</summary>
            public static Cord.Pages.S5StdManagePage S5StdManage
            {
                get { return GetPage<Cord.Pages.S5StdManagePage>(); }
            }
            /// <summary>Gets DIP Condition Manage Page.</summary>
            public static Cord.Pages.DippingStdManagePage DippingStdManage
            {
                get { return GetPage<Cord.Pages.DippingStdManagePage>(); }
            }
            /// <summary>Gets DIP Time Table Manage Page.</summary>
            public static Cord.Pages.DIPTimeTableStdPage DipTimeTableStdManage
            {
                get { return GetPage<Cord.Pages.DIPTimeTableStdPage>(); }
            }
            /// <summary>Gets DIP S8 Before Manage Page.</summary>
            public static Cord.Pages.S8BeforeConditionStdPage S8BeforeStdManage
            {
                get { return GetPage<Cord.Pages.S8BeforeConditionStdPage>(); }
            }
            /// <summary>Gets DIP S8 Product Condition Item Manage Page.</summary>
            public static Cord.Pages.S8ProductionConditionItemStdManagePage S8ProductionConditionItemStdManage
            {
                get { return GetPage<Cord.Pages.S8ProductionConditionItemStdManagePage>(); }
            }

            #endregion

            #region Document Menu (Cord) - Temporary

            /// <summary>Gets M3 Cord Document Menu Page.</summary>
            /*
            public static Cord.Pages.M3DocumentMenuPage M3CordDocumentMenu
            {
                get { return GetPage<Cord.Pages.M3DocumentMenuPage>(); }
            }
            */
            #endregion

            #region G4

            /// <summary>Gets G4 Receive Raw Material Page.</summary>
            public static Cord.Pages.G4ReceiveRawMaterialPage G4ReceiveRawMaterial
            {
                get { return GetPage<Cord.Pages.G4ReceiveRawMaterialPage>(); }
            }
            /// <summary>Gets G4 Issue Raw Material Page.</summary>
            public static Cord.Pages.G4IssueRawMaterialPage G4IssueRawMaterial
            {
                get { return GetPage<Cord.Pages.G4IssueRawMaterialPage>(); }
            }
            /// <summary>Gets G4 Stock Page.</summary>
            public static Cord.Pages.G4StockPage G4Stock
            {
                get { return GetPage<Cord.Pages.G4StockPage>(); }
            }
            /// <summary>Gets G4 Go Label Preview Page.</summary>
            public static Cord.Pages.GOLabelPreviewPage GoLabelPreview
            {
                get { return GetPage<Cord.Pages.GOLabelPreviewPage>(); }
            }
            /// <summary>Gets G4 Receive Return Meterial Page.</summary>
            public static Cord.Pages.G4ReceiveReturnMeterialPage G4ReceiveReturnMeterial
            {
                get { return GetPage<Cord.Pages.G4ReceiveReturnMeterialPage>(); }
            }

            #endregion

            #region Warehouse

            /// <summary>Gets WareHouse Receive Yarn Page.</summary>
            public static Cord.Pages.WarehouseReceiveYarnPage WarehouseReceiveYarn
            {
                get { return GetPage<Cord.Pages.WarehouseReceiveYarnPage>(); }
            }

            /// <summary>Gets WareHouse Check Stock Yarn Page.</summary>
            public static Cord.Pages.WarehouseYarnStockPage WarehouseYarnStock
            {
                get { return GetPage<Cord.Pages.WarehouseYarnStockPage>(); }
            }

            #endregion

            #region PCCard

            /// <summary>Gets PC Card Manage Page.</summary>
            public static Cord.Pages.PCCardManagePage PCCardManage
            {
                get { return GetPage<Cord.Pages.PCCardManagePage>(); }
            }

            #endregion

            #region First Twist

            /// <summary>Gets First Twist MC Page.</summary>
            public static Cord.Pages.FirstTwistMCPage FirstTwistMC
            {
                get { return GetPage<Cord.Pages.FirstTwistMCPage>(); }
            }
            /// <summary>Gets FirstTwist Operation Page.</summary>
            public static Cord.Pages.FirstTwistOperationPage FirstTwistOperation
            {
                get { return GetPage<Cord.Pages.FirstTwistOperationPage>(); }
            }

            /// <summary>Gets S1ProductCondition Page.</summary>
            /*
            public static Cord.Pages.S1ProductConditionPage S1ProductCondition
            {
                get { return GetPage<Cord.Pages.S1ProductConditionPage>(); }
            }
            */
            /// <summary>Gets S4x1UpTwisterConditionPage Page.</summary>
            /*
            public static Cord.Pages.S4x1UpTwisterConditionPage S4x1UpTwisterCondition
            {
                get { return GetPage<Cord.Pages.S4x1UpTwisterConditionPage>(); }
            }
            */
            /// <summary>Gets S4x2UpTwisterCondition Page.</summary>
            /*
            public static Cord.Pages.S4x2UpTwisterConditionPage S4x2UpTwisterCondition
            {
                get { return GetPage<Cord.Pages.S4x2UpTwisterConditionPage>(); }
            }
            */

            #endregion

            #region Pallet Setting

            /// <summary>Gets Pallet Setting Page.</summary>
            public static Cord.Pages.PalletSettingManagePage PalletSettingManage
            {
                get { return GetPage<Cord.Pages.PalletSettingManagePage>(); }
            }
            /// <summary>Gets M3 Cord PalletSetting Preview Page.</summary>
            public static Cord.Pages.PalletSettingPreviewPage PalletSettingPreview
            {
                get { return GetPage<Cord.Pages.PalletSettingPreviewPage>(); }
            }

            #endregion

            #region Ageing

            /// <summary>Gets Ageing Operation Page.</summary>
            public static Cord.Pages.AgeingOperationPage AgeingOperation
            {
                get { return GetPage<Cord.Pages.AgeingOperationPage>(); }
            }
            /// <summary>Gets Ageing Condition From Raw Material Page.</summary>
            public static Cord.Pages.AgeingConditionFromRawMaterialPage AgeingConditionFromRawMaterial
            {
                get { return GetPage<Cord.Pages.AgeingConditionFromRawMaterialPage>(); }
            }
            /// <summary>Gets Ageing Condition From Twisting Page.</summary>
            public static Cord.Pages.AgeingConditionFromTwistingPage AgeingConditionFromTwisting
            {
                get { return GetPage<Cord.Pages.AgeingConditionFromTwistingPage>(); }
            }
            /// <summary>Gets Ageing Condition From DIP Page.</summary>
            public static Cord.Pages.AgeingConditionFromDIPPage AgeingConditionFromDIP
            {
                get { return GetPage<Cord.Pages.AgeingConditionFromDIPPage>(); }
            }
            /// <summary>Gets S5 (Ageing) Report Preview Page.</summary>
            public static Cord.Pages.S5ReportPreviewPage S5ReportPreview
            {
                get { return GetPage<Cord.Pages.S5ReportPreviewPage>(); }
            }

            #endregion

            #region DIP

            /// <summary>Gets DIP MC Page.</summary>
            public static Cord.Pages.DIPMCPage DIPMC
            {
                get { return GetPage<Cord.Pages.DIPMCPage>(); }
            }
            /// <summary>Gets DIP Operation Page.</summary>
            public static Cord.Pages.DIPOperationPage DIPOperation
            {
                get { return GetPage<Cord.Pages.DIPOperationPage>(); }
            }
            /// <summary>Gets DIP Material Check Sheet Page.</summary>
            public static Cord.Pages.DIPMaterialCheckSheetPage DIPMaterialCheckSheet
            {
                get { return GetPage<Cord.Pages.DIPMaterialCheckSheetPage>(); }
            }
            /// <summary>Gets S7Creel Check Sheet Page.</summary>
            public static Cord.Pages.S7CreelCheckSheetPage S7CreelCheckSheet
            {
                get { return GetPage<Cord.Pages.S7CreelCheckSheetPage>(); }
            }
            /// <summary>Gets S8 Before Start Summary Page.</summary>
            public static Cord.Pages.S8BeforeStartSummaryPage S8BeforeStartSummary
            {
                get { return GetPage<Cord.Pages.S8BeforeStartSummaryPage>(); }
            }
            /// <summary>Gets DIP Condition Page.</summary>
            public static Cord.Pages.DIPConditionPage DIPCondition
            {
                get { return GetPage<Cord.Pages.DIPConditionPage>(); }
            }
            /// <summary>Gets S8 Condition Summary Page.</summary>
            public static Cord.Pages.S8ConditionSummaryPage S8ConditionSummary
            {
                get { return GetPage<Cord.Pages.S8ConditionSummaryPage>(); }
            }
            /// <summary>Gets Chemical Requisition Record Page.</summary>
            public static Cord.Pages.ChemicalRequisitionRecordPage ChemicalRequisitionRecord
            {
                get { return GetPage<Cord.Pages.ChemicalRequisitionRecordPage>(); }
            }
            /// <summary>Gets S9 Cleanning Check Sheet Page.</summary>
            public static Cord.Pages.S9CleanningCheckSheetPage S9CleanningCheckSheet
            {
                get { return GetPage<Cord.Pages.S9CleanningCheckSheetPage>(); }
            }
            /// <summary>Gets DIP Time Table Page.</summary>
            public static Cord.Pages.DIPTimeTablePage DIPTimeTable
            {
                get { return GetPage<Cord.Pages.DIPTimeTablePage>(); }
            }
            /// <summary>Gets S9 Appearance Check Sheet Page.</summary>
            public static Cord.Pages.S9AppearanceCheckSheetPage S9AppearanceCheckSheet
            {
                get { return GetPage<Cord.Pages.S9AppearanceCheckSheetPage>(); }
            }

            #endregion

            #region DIP Slip

            /// <summary>Gets DIP Pallet Slip Manage Page.</summary>
            public static Cord.Pages.DIPPalletSlipManagePage DIPPalletSlipManage
            {
                get { return GetPage<Cord.Pages.DIPPalletSlipManagePage>(); }
            }
            /// <summary>Gets M3 Cord DIP Pallet Slip Preview Page.</summary>
            public static Cord.Pages.DIPPalletSlipPreviewPage DIPPalletSlipPreview
            {
                get { return GetPage<Cord.Pages.DIPPalletSlipPreviewPage>(); }
            }

            #endregion

            #region Solition

            /// <summary>Gets M3 Cord Solition Slip Manage Page.</summary>
            public static Cord.Pages.SolutionSlipManagePage SolitionSlipManage
            {
                get { return GetPage<Cord.Pages.SolutionSlipManagePage>(); }
            }
            /// <summary>Gets M3 Cord Solition Slip Preview Page.</summary>
            public static Cord.Pages.SolutionSlipPreviewPage SolitionSlipPreview
            {
                get { return GetPage<Cord.Pages.SolutionSlipPreviewPage>(); }
            }

            public static Cord.Pages.CreateRecipePage CreateRecipe
            {
                get { return GetPage<Cord.Pages.CreateRecipePage>(); }
            }

            public static Cord.Pages.SolutionDetailPage SolutionDetail
            {
                get { return GetPage<Cord.Pages.SolutionDetailPage>(); }
            }
            public static Cord.Pages.RecordActualWeightPage RecordActualWeight
            {
                get { return GetPage<Cord.Pages.RecordActualWeightPage>(); }
            }

            #endregion

            #region LabelCHS9

            public static Cord.Pages.LabelCHS9PrintPreviewPage LabelCHS9PrintPreview
            {
                get { return GetPage<Cord.Pages.LabelCHS9PrintPreviewPage>(); }
            }

            #endregion

            #region QA

            public static Cord.Pages.CordSamplingQAManagePage CordSamplingQAManage
            {
                get { return GetPage<Cord.Pages.CordSamplingQAManagePage>(); }
            }

            public static Cord.Pages.CordSamplingQAPage CordSamplingQA
            {
                get { return GetPage<Cord.Pages.CordSamplingQAPage>(); }
            }

            #endregion

            #region First Twist View

            public static Cord.Pages.FirstTwistHistorySearchPage FirstTwistHistorySearch
            {
                get { return GetPage<Cord.Pages.FirstTwistHistorySearchPage>(); }
            }

            public static Cord.Pages.FirstTwistOperationViewPage FirstTwistOperationView
            {
                get { return GetPage<Cord.Pages.FirstTwistOperationViewPage>(); }
            }

            #endregion

            #region Ageing View

            /// <summary>Gets Ageing History Search Page.</summary>
            public static Cord.Pages.AgeingHistorySearchPage AgeingHistorySearch
            {
                get { return GetPage<Cord.Pages.AgeingHistorySearchPage>(); }
            }
            /// <summary>Gets Ageing Condition From Raw Material View Page.</summary>
            public static Cord.Pages.AgeingConditionFromRawMaterialViewPage AgeingConditionFromRawMaterialView
            {
                get { return GetPage<Cord.Pages.AgeingConditionFromRawMaterialViewPage>(); }
            }
            /// <summary>Gets Ageing Condition From Twisting View Page.</summary>
            public static Cord.Pages.AgeingConditionFromTwistingViewPage AgeingConditionFromTwistingView
            {
                get { return GetPage<Cord.Pages.AgeingConditionFromTwistingViewPage>(); }
            }
            /// <summary>Gets Ageing Condition From DIP View Page.</summary>
            public static Cord.Pages.AgeingConditionFromDIPViewPage AgeingConditionFromDIPView
            {
                get { return GetPage<Cord.Pages.AgeingConditionFromDIPViewPage>(); }
            }
            /// <summary>Gets S5 (Ageing) Report Preview View Page.</summary>
            public static Cord.Pages.S5ReportPreviewViewPage S5ReportPreviewView
            {
                get { return GetPage<Cord.Pages.S5ReportPreviewViewPage>(); }
            }

            #endregion

            #region DIP View

            public static Cord.Pages.DIPHistorySearchPage DIPHistorySearch
            {
                get { return GetPage<Cord.Pages.DIPHistorySearchPage>(); }
            }

            public static Cord.Pages.DIPOperationViewPage DIPOperationView
            {
                get { return GetPage<Cord.Pages.DIPOperationViewPage>(); }
            }

            #endregion

            #region Condition Document Preview

            public static Cord.Pages.S1ConditionPreviewPage S1ConditionPreview
            {
                get { return GetPage<Cord.Pages.S1ConditionPreviewPage>(); }
            }

            public static Cord.Pages.S4x1ConditionPreviewPage S4x1ConditionPreview
            {
                get { return GetPage<Cord.Pages.S4x1ConditionPreviewPage>(); }
            }

            public static Cord.Pages.S4x2ConditionPreviewPage S4x2ConditionPreview
            {
                get { return GetPage<Cord.Pages.S4x2ConditionPreviewPage>(); }
            }

            #endregion
        }

        /// <summary>
        /// Windows Static class.
        /// </summary>
        public static class Windows
        {
            #region Static Methods

            private static T GetWindow<T>()
                where T : Window, new()
            {
                T inst = new T();
                inst.Owner = Application.Current.MainWindow;
                return inst;
            }

            #endregion

            #region MessageBox

            /// <summary>Gets MessageBox Window.</summary>
            public static MessageBoxWindow MessageBox
            {
                get { return GetWindow<Cord.Windows.MessageBoxWindow>(); }
            }

            public static void SaveSuccess()
            {
                var win = M3CordApp.Windows.MessageBox;
                win.Setup("Save Success" + Environment.NewLine + "บันทึกข้อมูลสำเร็จ");
                win.ShowDialog();
            }

            public static void SaveFailed()
            {
                var win = M3CordApp.Windows.MessageBox;
                win.Setup("Save Failed" + Environment.NewLine + "บันทึกข้อมูลไม่สำเร็จ");
                win.ShowDialog();
            }

            #endregion

            #region MessageBoxOKCancel

            /// <summary>Gets MessageBoxOkCancel Window.</summary>
            public static MessageBoxOKCancelWindow MessageBoxOKCancel
            {
                get { return GetWindow<Cord.Windows.MessageBoxOKCancelWindow>(); }
            }

            #endregion

            #region SignIn

            /// <summary>Gets SignIn Window.</summary>
            public static SignInWindow SignIn
            {
                get { return GetWindow<SignInWindow>(); }
            }

            #endregion

            #region Import

            /// <summary>Gets Import Condition Std Window.</summary>
            public static ImportConditionStdWindow ImportConditionStd
            {
                get { return GetWindow<ImportConditionStdWindow>(); }
            }

            #endregion

            #region Common Progress Dialog

            /// <summary>Gets Common Progress Dialog Window.</summary>
            public static ProgressWindow ProgressDialog
            {
                get { return GetWindow<ProgressWindow>(); }
            }

            #endregion

            #region Import Error Dialog

            /// <summary>Gets Import Report Dialog Window.</summary>
            public static ImportReportWindow ImportReport
            {
                get { return GetWindow<ImportReportWindow>(); }
            }

            #endregion

            #region G4

            /// <summary>Gets G4 Receive Yarn Window.</summary>
            public static G4ReceiveYarnWindow G4ReceiveYarn
            {
                get { return GetWindow<G4ReceiveYarnWindow>(); }
            }

            #endregion

            #region PC Card

            /// <summary>Gets Cord Procuct Planning Window.</summary>
            public static PCCardEditorWindow PCCardEditor
            {
                get { return GetWindow<PCCardEditorWindow>(); }
            }

            #endregion

            #region First Twist

            /// <summary>Gets Choose PC Card for First Twist Window.</summary>
            public static ChoosePCCardTwist1Window ChoosePCCardTwist1
            {
                get { return GetWindow<ChoosePCCardTwist1Window>(); }
            }

            /// <summary>Gets Twist1 Load Record Editor Window.</summary>
            public static Twist1LoadRecordEditorWindow Twist1LoadRecordEditor
            {
                get { return GetWindow<Twist1LoadRecordEditorWindow>(); }
            }

            /// <summary>Gets Start Twist1 Op Window.</summary>
            public static StartTwist1OpWindow StartTwist1Op
            {
                get { return GetWindow<StartTwist1OpWindow>(); }
            }
            /// <summary>Gets End Twist1 Op Window.</summary>
            public static EndTwist1OpWindow EndTwist1Op
            {
                get { return GetWindow<EndTwist1OpWindow>(); }
            }

            /// <summary>Gets Twist 1 Check Sheet Window.</summary>
            public static Twist1CheckSheetEditorWindow Twist1CheckSheetEditor
            {
                get { return GetWindow<Twist1CheckSheetEditorWindow>(); }
            }

            #endregion

            #region Pallet Setting

            /// <summary>Gets Create Pallet Window.</summary>
            public static CreatePalletWindow CreatePallet
            {
                get { return GetWindow<CreatePalletWindow>(); }
            }
            /// <summary>Gets Choose PC TWist1 Window.</summary>
            public static ChoosePCTwist1Window ChoosePCTwist1
            {
                get { return GetWindow<ChoosePCTwist1Window>(); }
            }

            #endregion

            #region DIP

            /// <summary>Gets DIP PC Card Editor Window.</summary>
            public static DIPPCCardEditorWindow DIPPCCardEditor
            {
                get { return GetWindow<DIPPCCardEditorWindow>(); }
            }
            /// <summary>Gets Chemical Requisition Editor Window.</summary>
            public static ChemicalRequisitionEditorWindow ChemicalRequisitionEditor
            {
                get { return GetWindow<ChemicalRequisitionEditorWindow>(); }
            }

            /// <summary>Gets DIP TimeTable Editor Editor Window.</summary>
            public static DIPTimeTableEditorWindow DIPTimeTableEditor
            {
                get { return GetWindow<DIPTimeTableEditorWindow>(); }
            }

            /// <summary>Gets S8 Before Editor Window.</summary>
            public static S8BeforeEditorWindow S8BeforeEditor
            {
                get { return GetWindow<S8BeforeEditorWindow>(); }
            }

            /// <summary>Gets S8 Production Condition Item Editor Window.</summary>
            public static S8ProductionConditionItemEditorWindow S8ProductionConditionItemEditor
            {
                get { return GetWindow<S8ProductionConditionItemEditorWindow>(); }
            }

            
            #endregion

            #region DIP Slip

            /// <summary>Gets Create DIP Slip Window.</summary>
            public static CreateDIPSlipWindow CreateDIPSlip
            {
                get { return GetWindow<CreateDIPSlipWindow>(); }
            }

            #endregion

            #region Solution

            public static SolutionWeightWindow SolutionWeight
            {
                get { return GetWindow<SolutionWeightWindow>(); }
            }

            #endregion

            #region LabelCHS9

            /// <summary>Gets LabelCHS9 Search Window.</summary>
            public static LabelCHS9SearchWindow LabelCHS9Search
            {
                get { return GetWindow<LabelCHS9SearchWindow>(); }
            }

            #endregion

            #region First Twist View

            /// <summary>Gets Twist1 Load Record View Window.</summary>
            public static Twist1LoadRecordViewWindow Twist1LoadRecordView
            {
                get { return GetWindow<Twist1LoadRecordViewWindow>(); }
            }

            #endregion
        }
    }
}
