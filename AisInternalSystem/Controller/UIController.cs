using AisInternalSystem.Controller;
using AisInternalSystem.UserInterface;
using AisInternalSystem.UserInterface.Menu;
using AisInternalSystem.UserInterface.Student;
using AisInternalSystem.UserInterface.Employee;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace AisInternalSystem.Controller
{
    public class UIController
    {
        #region Props
        private static BackgroundWorker worker;
        private static UpperPanel upperdefault = new UpperPanel();
        private static UpperPanelAdmin upperPanelAdmin = new UpperPanelAdmin();
        public static LoginFrm login = new LoginFrm();
        private static DashboardUC ucDashboardAdmin = new DashboardUC();
        private static MenuContainer menuContainer = new MenuContainer();
        private static UCRecStudent studUI = new UCRecStudent();
        private static Liner liner = new Liner();
        private static DialogControl dialogConfirmation = new DialogControl();
        private static UCInventory InventoryDirectory = new UCInventory();
        private static UCClassDirectoryService classDirService = new UCClassDirectoryService();
        private static UCClassView classView = new UCClassView();
        private static UCStudDirectory StudentDirectory = new UCStudDirectory();
        private static UCSubject subjectDirectory = new UCSubject();
        private static UserInterface.Settings.UserSettings userSettings = new UserInterface.Settings.UserSettings();
        private static UCStudDetailed StudDetailed = new UCStudDetailed();
        private static EmployeeDirectory EmployeeDirectoryService = new EmployeeDirectory();
        private static EmployeeRec EmployeeDataRecUpdate = new EmployeeRec();
        private static List<UserControl> controls = new List<UserControl>();
        #endregion

        public static event EventHandler FinishedLoadingObject;

        private static List<Guna2Button> CategoryButton()
        {
            List<Guna2Button> buttons = new List<Guna2Button>();
            for (int i = 0; i < Menus.Category.Count; i++)
            {
                buttons.Add(Menus.Category[i].CategoryHandler);
            }
            return buttons;
        }

        public static void CategoryClicked(CategoryMenu menu)
        {
            HighlightButton(CategoryButton(), menu.CategoryHandler);
            GetLiner(menu.CategoryHandler);
            GetMenu(menu);
        }

        public static void ResetMenu()
        {
            menuContainer.Visible = false;
            liner.Visible = false;
        }

        private static void InitWorkerUI()
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

        }

        private static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        
        public static void init()
        {

        }

        private static void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private static void IsWaiting(bool tr)
        {
            mainform.Invoke(new MethodInvoker(delegate { mainform.Controls["Waiter"].Visible = tr; }));

        }

        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            IsWaiting(true);
        }

        #region Enumeration
        public enum Controls
        {
            UpperPanel, UpperPanelAdmin, UpperPanelManagement, UpperPanelTeacher,
            UCDashboardAdmin, UCDashboardManagement, UCDashboardTeacher, UCDashboardAccounting,
            UCLogin,
            EmployeeDirectoryService, MenuContainer, RecordEmployee, StudentDirectoryService, EditEmployee,
            RecordStudentData, UpdateStudentData, ClassDirectoryService, ClassView, SubjectDirectoryServices,
            DialogConfirmation, InventoryDirectory, UserSettings, StudDetailed, EmployeeDetailed
        }

        public static void Navigation(UserControl ctrl, DockStyle dock)
        {
            mainform.Controls.Add(ctrl);
            ctrl.Dock = dock;
        }

        private static void initObject()
        {

        }

        public enum ControlState
        {
            Load, Dispose   
        }

        #endregion

        #region Properties
        public UIController()
        {

        }

        #endregion

        #region Variable
        private static Form mainform = Application.OpenForms[0];
        private static Controls _controls;
        public static int _memId;
        #endregion

        #region Function
        public static void HighlightButton(List<Guna2Button> buttons, Guna2Button btnFocused)
        {
            foreach (Guna2Button item in buttons)
            {
                item.FillColor = PublicProperties.NavNormalColorButton;
                item.ForeColor = PublicProperties.NavNormalForeColor;
            }
            btnFocused.FillColor = PublicProperties.NavFocusedColorButton;
            btnFocused.ForeColor = PublicProperties.NavFocusedForeColor;
        }


        public static void SendPanelBack(Guna2ShadowPanel panel)
        {
            panel.SendToBack();
        }

        public static void BringToFront(Guna2ShadowPanel panel)
        {
            panel.BringToFront();
        }

        public static void SendContainerBack(Panel panel)
        {
            panel.SendToBack();
        }

        public static void AddControlToMainForm(UserControl ctrl, DockStyle dock)
        {
            mainform.Controls.Add(ctrl);
            SetControl(ctrl, dock);
        }

        public static void BringContainerFront(Panel panel)
        {
            panel.BringToFront();
        }


        public static void DisposeMenu(Guna2ShadowPanel sender)
        {
            //mainform.Controls.Remove(menuContainer);
            //mainform.Controls.Remove(liner);
            //ClearFocusButton(sender);
            
        }

        public static List<Guna2Button> btnDotted = new List<Guna2Button>();

        public static void OverrideControl(System.Windows.Forms.UserControl uc, DockStyle dock)
        {
            mainform.Controls.Add(uc);
            SetControl(uc, dock);
        }

        private static void AddNavigation(UserControl control, DockStyle dock)
        {
            if (IsLoaded(control))
            {
                SetControl(control, dock);
            }
            else
            {
                mainform.Controls.Add(control);
                SetControl(control, dock);
            }
        }

        public static void ClassChoosed(UCClassModel classmodel)
        {
            classDirService.ClassChoosedEvent(classmodel);
        }

        public static void TeacherRevoke(UCSubjectTeacher teacher)
        {
            subjectDirectory.TeacherRevoke(teacher);
        }

        public static void SubjectChoosed(UCSubjectList subject)
        {
            subjectDirectory.SubjectChoosedEvent(subject);
        }

        public enum stateofControlEnum
        {
            Iddle, Focused
        }
        private static Guna2DragControl dragger = new Guna2DragControl();

        public static void GetDragControl(Guna2ShadowPanel targetPanel)
        {
            dragger.TargetControl = targetPanel;
        }

        public static void ImageButtonZoom(PictureBox pic, stateofControlEnum state)
        {
            switch (state)
            {
                case stateofControlEnum.Iddle:
                    pic.Height -= 10;
                    pic.Width -= 10;
                    break;
                case stateofControlEnum.Focused:
                    pic.Height += 10;
                    pic.Width += 10;
                    break;
                default:
                    break;
            }
        }

        public static void MinimizeApp()
        {
            mainform.WindowState = FormWindowState.Minimized;
            PopUp.Alert("The application is now minimized!", frmAlert.AlertType.Info);
        }

        public static void StartUI()
        {
            UIController.NavigateUI(UIController.Controls.UpperPanel);
        }

        public static void NavigateUI(Controls controls)
        {
            _controls = controls;
            switch (controls)
            {
                case Controls.EmployeeDirectoryService:
                    AddNavigation(EmployeeDirectoryService, DockStyle.Fill);
                    EmployeeDirectoryService.InitObject();
                    break;
                case Controls.RecordEmployee:
                    AddNavigation(EmployeeDataRecUpdate, DockStyle.Fill);
                    EmployeeDataRecUpdate.InitObject(EmployeeRec.EditMode.Create);
                    break;
                case Controls.EditEmployee:
                    AddNavigation(EmployeeDataRecUpdate, DockStyle.Fill);
                    EmployeeDataRecUpdate.InitObject(EmployeeRec.EditMode.Update);
                    break;
                case Controls.UserSettings:
                    AddNavigation(userSettings, DockStyle.Fill);
                    userSettings.InitObject();
                    break;
                case Controls.UpperPanel:
                    AddNavigation(upperdefault, DockStyle.Top);
                    upperdefault.InitializeObject();
                    break; 
                case Controls.UpperPanelAdmin:
                    if(IsLoaded(upperPanelAdmin))
                    {
                        SetControl(upperPanelAdmin, DockStyle.Top);
                    }
                    else
                    {
                        DisposeControl(upperdefault);
                        mainform.Controls.Add(upperPanelAdmin);
                        mainform.Controls.Add(menuContainer);
                        upperPanelAdmin.InitializeObject();
                        upperPanelAdmin.imgLocation = Data.user.UserImage;
                        SetControl(upperPanelAdmin, DockStyle.Top);
                    }
                    break;
                case Controls.UpperPanelManagement:

                    break;
                case Controls.UpperPanelTeacher:

                    break;
                case Controls.ClassDirectoryService:
                    AddNavigation(classDirService, DockStyle.Fill);
                    classDirService.InitObject(ControlState.Load);
                    break;
                case Controls.SubjectDirectoryServices:
                    AddNavigation(subjectDirectory, DockStyle.Fill);
                    subjectDirectory.InitObject();
                    break;
                case Controls.ClassView:
                    AddNavigation(classView, DockStyle.Fill);
                    classView.InitObject();
                    break;
                case Controls.UCDashboardAdmin:
                    if (IsLoaded(ucDashboardAdmin))
                    {
                        SetControl(ucDashboardAdmin, DockStyle.Fill);
                    }
                    else
                    {
                        mainform.Controls.Add(ucDashboardAdmin);
                        ucDashboardAdmin.InitializeObject();
                        ucDashboardAdmin.NameUser = Data.user.OwnerName;
                        ucDashboardAdmin.UserRole = Data.user.Roles;
                        SetControl(ucDashboardAdmin, DockStyle.Fill);
                    }
                    break;
                case Controls.UCLogin:
                    if (IsLoaded(login))
                    {
                        SetControl(login, DockStyle.Fill);
                    }
                    else
                    {
                        mainform.Controls.Add(login);
                        mainform.Controls.Add(menuContainer);
                        login.InitializeObject();
                        SetControl(login, DockStyle.Fill);
                    }
                    break;
                case Controls.MenuContainer:
                    if (IsLoaded(menuContainer))
                    {
                        SetControl(menuContainer, DockStyle.None);
                    }
                    else
                    {
                        SetControl(menuContainer, DockStyle.None);
                    }
                    break;
                case Controls.RecordStudentData:
                    AddNavigation(studUI, DockStyle.Fill);
                    studUI.InitObject(UCRecStudent.EditMode.Create);
                    break;
                case Controls.UpdateStudentData:
                    AddNavigation(studUI, DockStyle.Fill);
                    studUI.InitObject(UCRecStudent.EditMode.Edit);
                    break;
                case Controls.DialogConfirmation:
                    AddNavigation(dialogConfirmation, DockStyle.Fill);
                    break;
                case Controls.InventoryDirectory:
                    AddNavigation(InventoryDirectory, DockStyle.Fill);
                    InventoryDirectory.InitObject();
                    break;
                case Controls.StudentDirectoryService:
                    AddNavigation(StudentDirectory, DockStyle.Fill);
                    StudentDirectory.InitObject();
                    break;
            }
        }

        public static void NormalizeButton(Guna2Button button, Color color)
        {
            button.FillColor = color;
            button.ForeColor = Color.Black;
        }


        public static void FocusButton(Guna2Button button, Guna2ShadowPanel panel)
        {
            foreach (var item in panel.Controls)
            {
                if (item is Guna2Button)
                {
                    Guna2Button buttons = item as Guna2Button;
                    buttons.FillColor = Color.White;
                    buttons.ForeColor = Color.Black;
                }
                button.FillColor = Color.DarkGray;
                button.ForeColor = Color.Black;
            }
        }

        public static void ClearFocusButton(Guna2ShadowPanel panel)
        {
            foreach (var item in panel.Controls)
            {
                if (item is Guna2Button)
                {
                    Guna2Button buttons = item as Guna2Button;
                    buttons.FillColor = Color.White;
                    buttons.ForeColor = Color.Black;
                }
            }
            foreach (Guna2Button itemDotted in btnDotted)
            {
                Guna2Button btn = itemDotted;
                btn.FillColor = Color.LightCoral;
            }
        }

        public static void GetMenu(CategoryMenu menu)
        {
            menuContainer.Visible = true;
            if (IsLoaded(menuContainer))
            {
                SetControl(menuContainer, DockStyle.None);
                menuContainer.InitObject(menu);

            }
            else
            {
                menuContainer.InitObject(menu);
                SetControl(menuContainer, DockStyle.None);

            }
        }



        public static void GetLiner(Guna2Button btn)
        {
            liner.Visible = true;
            Point location = new Point(btn.Location.X + (btn.Width/2) - 10, ((Guna2ShadowPanel)btn.Parent).Height - liner.Height - 7);
            if (IsLoaded(liner))
            {
                SetControl(liner, DockStyle.None);
                liner.BackColor = Color.White;
                liner.Location = location;
            }
            else
            {
                mainform.Controls.Add(liner);
                SetControl(liner, DockStyle.None);
                liner.Location = location;
                liner.BackColor = Color.White;
            }
        }

        private static bool IsLoaded(System.Windows.Forms.UserControl userControl)
        {
            if(mainform.Controls.Contains(userControl))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void DisposeControl(System.Windows.Forms.UserControl controls)
        {
            mainform.Controls.Remove(controls);
        }

        public static void AnimateControl(Control control, Guna.UI2.AnimatorNS.AnimationType type)
        {
            control.Visible = false;
            Transition.AnimationType = type;
            Transition.Show(control);
        }

        public static void AnimateShadowPanel(Guna2ShadowPanel control, Guna.UI2.AnimatorNS.AnimationType type)
        {
            control.Visible = false;
            Transition.AnimationType = type;
            Transition.Show(control);
        }

        public static void AnimateHideShadowPanel(Guna2ShadowPanel control, Guna.UI2.AnimatorNS.AnimationType type)
        {
            Transition.AnimationType = type;
            Transition.HideSync(control);
        }
        public static void Animate(UserControl control, Guna.UI2.AnimatorNS.AnimationType type)
        {
            control.Visible = false;
            Transition.AnimationType = type;
            Transition.Show(control);
        }
        public static void AnimateHide(UserControl control, Guna.UI2.AnimatorNS.AnimationType type)
        {
            Transition.AnimationType = type;
            Transition.Hide(control);
        }
        private static Guna2Transition Transition = new Guna2Transition();
        private static void SetControl(System.Windows.Forms.UserControl control, DockStyle dock)
        {
            control.BringToFront();
            switch (dock)
            {
                case DockStyle.None:
                    control.Dock = DockStyle.None;
                    break;
                case DockStyle.Top:
                    control.Dock = DockStyle.Top;
                    break;
                case DockStyle.Bottom:
                    control.Dock = DockStyle.Bottom;
                    break;
                case DockStyle.Left:
                    control.Dock = DockStyle.Left;
                    break;
                case DockStyle.Right:
                    control.Dock = DockStyle.Right;
                    break;
                case DockStyle.Fill:
                    control.Dock = DockStyle.Fill;
                    break;
                default:
                    break;
            }
            //Transition.Dispose();
        }

        #endregion

    }
}
