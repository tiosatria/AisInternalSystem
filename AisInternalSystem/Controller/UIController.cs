using AisInternalSystem.Controller;
using AisInternalSystem.UserInterface;
using AisInternalSystem.UserInterface.Menu;
using AisInternalSystem.UserInterface.Student;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Windows.Markup.Localizer;
using Telerik.WinControls.FileDialogs;
using Telerik.WinControls.UI;
using MenuItem = AisInternalSystem.UserInterface.Menu.MenuItem;

namespace AisInternalSystem.Entities
{
    public class UIController
    {
        #region Props
        private static BackgroundWorker worker;
        private static UpperPanel upperdefault = new UpperPanel();
        private static UpperPanelAdmin upperPanelAdmin = new UpperPanelAdmin();
        public static LoginFrm login = new LoginFrm();
        private static DashboardUC ucDashboardAdmin = new DashboardUC();
        private static MenuSchoolAdministration menuSchoolAdm = new MenuSchoolAdministration();
        private static MenuContainer menuContainer = new MenuContainer();
        private static MenuItem menuItem = new MenuItem();
        private static UCEmployeeDetailed employeeDetailed = new UCEmployeeDetailed();
        private static UCRecStudent studUI = new UCRecStudent();
        private static Liner liner = new Liner();
        private static DialogControl dialogConfirmation = new DialogControl();
        private static UCClassDirectoryService classDirService = new UCClassDirectoryService();
        private static UCClassView classView = new UCClassView();
        private static UCSubject subjectDirectory = new UCSubject();
        private static List<UserControl> controls = new List<UserControl>();
        private MenuController.MenuType _menutype;
        #endregion

        public static event EventHandler FinishedLoadingObject;

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
            MenuSchoolAdm, MenuEmployee, MenuContainer,
            RecordStudentData, UpdateStudentData, ClassDirectoryService, ClassView, SubjectDirectoryServices,
            DialogConfirmation
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

        private static List<Dotter> dotters = new List<Dotter>();
        private static List<TaskExpander> TaskExpander = new List<TaskExpander>();

        public static void OpenDialog()
        {
            
        }

        public static void InitTask(MenuController.MenuType _men, Guna2Button sender)
        {
            if (dotters.Exists(o => o.FromGroup == _men))
            {
                dotters[dotters.FindIndex(o => o.FromGroup == _men)].TaskCount++;

            }
            else
            {
                Dotter dotter = new Dotter();
                TaskExpander expander = new TaskExpander(_men);
                dotter.TaskCount ++;
                dotter.FromGroup = _men;
                dotter.Location = new Point (sender.Location.X + 5, ((Guna2ShadowPanel)sender.Parent).Height - dotter.Height - 7);
                expander.FromGroup = _men;
                expander.Location = new Point(sender.Location.X, dotter.Location.Y + dotter.Height);
                dotters.Add(dotter);
                mainform.Controls.Add(dotter);
                mainform.Controls.Add(expander);
                Data.taskExpanders.Add(expander);
                DotterLogic(_men, sender);
                SetControl(expander, DockStyle.None);
                SetControl(dotter, DockStyle.None);
            }
        }

        private static List<Guna2Button> btnDotted = new List<Guna2Button>();

        private static void DotterLogic(MenuController.MenuType _men, Guna2Button sender)
        {
            if (dotters[dotters.FindIndex(o => o.FromGroup == _men)].TaskCount >= 1)
            {
                btnDotted.Add(sender);
            }
            else
            {
                btnDotted.Remove(sender);
            }
            foreach (Guna2Button button in btnDotted)
            {
                button.FillColor = Color.LightCoral;
            }
        }

        public static void ResetMenu()
        {
            ClearFocusButton((Guna2ShadowPanel)_membtn.Parent);
            liner.Visible = false;
            menuContainer.Visible = false;
        }
        private static MenuController.MenuType _memmenu;
        private static Guna2Button _membtn;

        public static MenuController.MenuType GetGroup()
        {
            return _memmenu;
        }


        public static Guna2Button SenderButton()
        {
            return _membtn;
        }

        public static void MenuChoosed(MenuController.MenuType menuType, Guna2Button button)
        {
            _membtn = button;
            _memmenu = menuType;
            UIController.FocusButton(button, (Guna2ShadowPanel)button.Parent);
            UIController.GetLiner(button);
            UIController.GetMenu(menuType);
        }

        public static void OverrideControl(System.Windows.Forms.UserControl uc, DockStyle dock)
        {
            mainform.Controls.Add(uc);
            SetControl(uc, dock);
        }

        private static void AddNavigation(UserControl control)
        {
            if (IsLoaded(control))
            {
                SetControl(control, DockStyle.Fill);
            }
            else
            {
                mainform.Controls.Add(control);
                SetControl(control, DockStyle.Fill);
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

        public static void NavigateUI(Controls controls)
        {
            _controls = controls;
            switch (controls)
            {
                case Controls.UpperPanel:
                    if (IsLoaded(upperdefault))
                    {
                        SetControl(upperdefault, DockStyle.Top);
                    }
                    else
                    {
                        mainform.Controls.Add(upperdefault);
                        upperdefault.InitializeObject();
                        SetControl(upperdefault, DockStyle.Top);
                    }
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
                    AddNavigation(classDirService);
                    classDirService.InitObject(ControlState.Load);

                    break;
                case Controls.SubjectDirectoryServices:
                    AddNavigation(subjectDirectory);
                    subjectDirectory.InitObject();
                    break;
                case Controls.ClassView:
                    AddNavigation(classView);
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
                case Controls.MenuSchoolAdm:
                    if (IsLoaded(menuSchoolAdm))
                    {
                        SetControl(menuSchoolAdm, DockStyle.Fill);
                    }
                    else
                    {
                        mainform.Controls.Add(menuSchoolAdm);
                        SetControl(menuSchoolAdm, DockStyle.Fill);
                    }
                    break;
                case Controls.MenuEmployee:
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
                    AddNavigation(studUI);
                    SetControl(studUI, DockStyle.Fill);
                    studUI.InitObject();
                    break;
                case Controls.UpdateStudentData:

                    break;
                case Controls.DialogConfirmation:
                    if (IsLoaded(dialogConfirmation))
                    {
                        SetControl(dialogConfirmation, DockStyle.Fill);
                    }
                    else
                    {
                        mainform.Controls.Add(dialogConfirmation);
                        SetControl(dialogConfirmation, DockStyle.Fill);
                    }
                    break;
                default:

                    break;
            }
        }

        public static void NormalizeButton(Guna2Button button, Color color)
        {
            button.FillColor = color;
            button.ForeColor = Color.Black;
        }

        public static void HighlightButton(Guna2Button button, Color color)
        {
            button.FillColor = color;
            button.ForeColor = Color.White;
        }

        public static void FocusButtonNoPanel(Guna2Button btn, UserControl control)
        {
            foreach (var item in control.Controls)
            {
                if (item is Guna2Button)
                {
                    Guna2Button button = item as Guna2Button;
                    if (button.FillColor == Color.Silver)
                    {
                        button.FillColor = Color.Silver;
                        button.ForeColor = Color.Black;
                    }
                    else if (button.FillColor == Color.Black)
                    {
                        button.FillColor = Color.Silver;
                        button.ForeColor = Color.Black;
                    }
                }

            }
            btn.FillColor = Color.Black;
            btn.ForeColor = Color.White;
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

        public static void GetMenu(MenuController.MenuType menu)
        {
            menuContainer.Visible = true;
            if (IsLoaded(menuContainer))
            {
                SetControl(menuContainer, DockStyle.None);
                menuContainer.InitObject(menu);

            }
            else
            {
                mainform.Controls.Add(menuContainer);
                SetControl(menuContainer, DockStyle.None);
                menuContainer.InitObject(menu);
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

        public static void Animate(UserControl control, Guna.UI2.AnimatorNS.AnimationType type)
        {
            control.Visible = false;
            Transition.AnimationType = type;
            Transition.Show(control);
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
