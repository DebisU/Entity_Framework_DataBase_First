using Entty_Framework_Model_First.Persistance;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Entity_Framework_Model_First
{


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadToGrid();
        }

        private int TransformStringIntoInt(string input)
        {
            int output;
            int.TryParse(input, out output);
            return output;
        }

        private void BtInsert_Click(object sender, RoutedEventArgs e)
        {
            model_first_migration_testEntities testcontext = new model_first_migration_testEntities();
            try
            {

                employee emp = new employee

                {
                    Name = TbName.Text,
                    BirthYear = TransformStringIntoInt(TbBirthtYear.Text)
                };
                testcontext.employee.Add(emp);
                testcontext.SaveChanges();
                MessageBox.Show("Record Inserted successfully.");
                LoadToGrid();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        private void BtUpdate_Click(object sender, RoutedEventArgs e)
        {
            int EmpId = TransformStringIntoInt(TbId.Text);

            model_first_migration_testEntities testcontext = new model_first_migration_testEntities();
            try
            {
                employee emp = testcontext.employee.First(i => i.ID == EmpId);
                {
                    emp.Name = TbName.Text;

                    int birth;
                    string b = TbBirthtYear.Text;
                    int.TryParse(b, out birth);
                    emp.BirthYear = birth;

                    testcontext.SaveChanges();
                    MessageBox.Show("Record Updated successfully.");
                    LoadToGrid();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            int EmpId = TransformStringIntoInt(TbId.Text);

            model_first_migration_testEntities testcontext = new model_first_migration_testEntities();
            try
            {
                employee emp = testcontext.employee.FirstOrDefault(i => i.ID == EmpId);
                testcontext.employee.Remove(emp);
                testcontext.SaveChanges();
                MessageBox.Show("Record Deleted successfully.");
                LoadToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        private void LoadToGrid()
        {
            model_first_migration_testEntities testcontext = new model_first_migration_testEntities();
            var load = from g in testcontext.employee select g;
            if (load != null)
            {
                dataGrid.ItemsSource = load.ToList();
            }
        }

        private void dataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TbId.Text = ((employee)dataGrid.SelectedItem).ID.ToString();
            TbName.Text = ((employee)dataGrid.SelectedItem).Name.ToString();
            TbBirthtYear.Text = ((employee)dataGrid.SelectedItem).BirthYear.ToString();
        }
    }
}
