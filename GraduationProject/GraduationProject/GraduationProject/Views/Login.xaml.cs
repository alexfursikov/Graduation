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

namespace GraduationProject.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //using (CRMContext dbContext = new CRMContext())
            {
                // foreach (var item in dbContext.Managers)
                {
                    //  if(Hash.PasswordsMatch(Hash.EncryptPassword(loginTextBox.Text,passwordPasswordBox.Password),item.Password))
                    {

                        // if (item.Group != "Уволенные")
                        //{   if (item.Position == "Администратор") IAm.isAdmin = true;
                        //  IAm.myName = item.Name;
                        //Popup1.IsOpen = true;
                        var mainWnd = new MainWindow();
                        mainWnd.Show();
                        this.Close();
                        // EmailSender.SendMail("n.harseko@mail.ru", "w5323871w", item.Email, "Вход", "Вы вошли в систему под логином: " + item.Login);
                        //}
                    }
                    //else
                    {
                          
                    }
                }

            }

        }
    }
}
