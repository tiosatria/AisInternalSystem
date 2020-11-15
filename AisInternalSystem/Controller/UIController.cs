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
        private static MenuSchoolAdministration menuSchoolAdm = new MenuSchoolAdministration();
        private static MenuContainer menuContainer = new MenuContainer();
        private static MenuItem menuItem = new MenuItem();
        private static UCEmployeeDetailed employeeDetailed = new UCEmployeeDetailed();
        private static UCRecStudent studUI = new UCRecStudent();
        private static Liner liner = new Liner();
        private static DialogControl dialogConfirmation = new DialogControl();
        private static UCInventory InventoryDirectory = new UCInventory();
        private static UCClassDirectoryService classDirService = new UCClassDirectoryService();
        private static UCClassView classView = new UCClassView();
        private static UCSubject subjectDirectory = new UCSubject();
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
            MenuSchoolAdm, EmployeeDirectoryService, MenuContainer, RecordEmployee, StudentDirectoryService,
            RecordStudentData, UpdateStudentData, ClassDirectoryService, ClassView, SubjectDirectoryServices,
            DialogConfirmation, InventoryDirectory
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
                case Controls.InventoryDirectory:
                    AddNavigation(InventoryDirectory);
                    SetControl(InventoryDirectory, DockStyle.Fill);
                    InventoryDirectory.InitObject();
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
