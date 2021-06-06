using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Models;

namespace ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Controllers
{
    public class ReportController : Controller
    {
        Entities dbModel = new Entities();
        private SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-V217U6T;Initial Catalog=PatientFileBackup2;Integrated Security=True");
        public ActionResult Index(Логин_и_пароль auth)
        {
            //var userDetails = dbModel.Логин_и_пароль.Where(x => x.Логин == auth.Логин && x.Пароль == auth.Пароль).FirstOrDefault();
            //Session["Код"] = userDetails.Код;

            //if (userDetails.Код == 1)
            //{ 
                
            //}
            return View();
        }
        public ActionResult Stats()
        {
            return View();
        }
        #region 
        public ActionResult PatFiles()
        {
            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                //добавляем книгу
                excelApplication.Workbooks.Add(Type.Missing);

                //делаем временно неактивным документ
                excelApplication.Interactive = false;
                excelApplication.EnableEvents = false;

                //выбираем лист на котором будем работать (Лист 1)
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApplication.Sheets[1];
                //Название листа
                excelSheet.Name = "Список ЭМК пациентов";

                //Выгрузка данных из таблицы данных, заполненной c помощью sql-запроса
                System.Data.DataTable excelDataTable = GetPatientFileList();

                int columnIndex = 0;
                int rowIndex = 0;
                string adressData = "";
                Microsoft.Office.Interop.Excel.Range excelSheeetRange;
                //заполнение колонок листа excel
                for (int i = 0; i < excelDataTable.Columns.Count; i++)
                {
                    adressData = excelDataTable.Columns[i].ColumnName.ToString();
                    excelSheet.Cells[1, i + 1] = adressData;

                    //определяется диапазон ячеек
                    excelSheeetRange = excelSheet.get_Range("A1:Z1", Type.Missing);

                    excelSheeetRange.WrapText = true;
                    excelSheeetRange.Font.Bold = true;
                }

                //заполняем строки
                for (rowIndex = 0; rowIndex < excelDataTable.Rows.Count; rowIndex++)
                {
                    for (columnIndex = 0; columnIndex < excelDataTable.Columns.Count; columnIndex++)
                    {
                        adressData = excelDataTable.Rows[rowIndex].ItemArray[columnIndex].ToString();
                        excelSheet.Cells[rowIndex + 2, columnIndex + 1] = adressData;
                    }
                }

                //выбираем всю область данных
                excelSheeetRange = excelSheet.UsedRange;

                //выравниваем строки и колонки по их содержимому
                excelSheeetRange.Columns.AutoFit();
                excelSheet.Rows.AutoFit();

            }
            catch (Exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Проверьте данные на дублирование или обратитесь в техническую поддержку!');</script>");
            }
            finally
            {
                //Показываем ексель
                excelApplication.Visible = true;

                excelApplication.Interactive = true;
                excelApplication.ScreenUpdating = true;
                excelApplication.UserControl = true;
            }
            return RedirectToAction("Index");
        }

        private System.Data.DataTable GetPatientFileList()
        {
            string excelConnString = @"Data Source=DESKTOP-V217U6T;Initial Catalog=PatientFileBackup2;Integrated Security=True";

            SqlConnection excelLoadConn = new SqlConnection(excelConnString);

            System.Data.DataTable excelDataTable = new System.Data.DataTable();
            try
            {
                string adressQuery = @"SELECT[Электронная амбулаторная карта].[Номер амбулаторной карты], [Учреждение здравоохранения].[Название учреждения], [Электронная амбулаторная карта].Фамилия, [Электронная амбулаторная карта].Имя, [Электронная амбулаторная карта].Отчество, [Электронная амбулаторная карта].[Дата рождения], [Электронная амбулаторная карта].Пол, [Электронная амбулаторная карта].[Номер полиса ОМС], [Электронная амбулаторная карта].[Номер СНИЛС], [Электронная амбулаторная карта].[Серия и номер паспорта], [Электронная амбулаторная карта].[Серия и номер свидетельства о рождении пациента]

FROM[Учреждение здравоохранения] INNER JOIN[Электронная амбулаторная карта] ON[Учреждение здравоохранения].[Код учреждения] = [Электронная амбулаторная карта].[Код учреждения здравоохранения];

";
                SqlCommand adressLoadComm = new SqlCommand(adressQuery, excelLoadConn);

                excelLoadConn.Open();
                SqlDataAdapter adressDataAdapter = new SqlDataAdapter(adressLoadComm);
                DataSet adressDataSet = new DataSet();
                adressDataAdapter.Fill(adressDataSet);
                excelDataTable = adressDataSet.Tables[0];
            }
            catch (Exception)
            {
               // return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Проверьте данные на дублирование или обратитесь в техническую поддержку!');</script>");
            }
            finally
            {
                excelLoadConn.Close();
                excelLoadConn.Dispose();
            }
            return excelDataTable;
        }
        #endregion

        #region
        public ActionResult DocList()
        {
            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                excelApplication.Workbooks.Add(Type.Missing);

                excelApplication.Interactive = false;
                excelApplication.EnableEvents = false;

                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApplication.Sheets[1];

                excelSheet.Name = "Список врачей";

                System.Data.DataTable excelDataTable = GetDoctorListData();

                int columnIndex = 0;
                int rowIndex = 0;
                string adressData = "";
                Microsoft.Office.Interop.Excel.Range excelSheeetRange;
                for (int i = 0; i < excelDataTable.Columns.Count; i++)
                {
                    adressData = excelDataTable.Columns[i].ColumnName.ToString();
                    excelSheet.Cells[1, i + 1] = adressData;

                    excelSheeetRange = excelSheet.get_Range("A1:Z1", Type.Missing);

                    excelSheeetRange.WrapText = true;
                    excelSheeetRange.Font.Bold = true;
                }
                for (rowIndex = 0; rowIndex < excelDataTable.Rows.Count; rowIndex++)
                {
                    for (columnIndex = 0; columnIndex < excelDataTable.Columns.Count; columnIndex++)
                    {
                        adressData = excelDataTable.Rows[rowIndex].ItemArray[columnIndex].ToString();
                        excelSheet.Cells[rowIndex + 2, columnIndex + 1] = adressData;
                    }
                }

                excelSheeetRange = excelSheet.UsedRange;
                excelSheeetRange.Columns.AutoFit();
                excelSheet.Rows.AutoFit();

            }
            catch (Exception )
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Проверьте данные на дублирование или обратитесь в техническую поддержку!');</script>");
            }
            finally
            {
                excelApplication.Visible = true;

                excelApplication.Interactive = true;
                excelApplication.ScreenUpdating = true;
                excelApplication.UserControl = true;
            }
            return RedirectToAction("Index");

        }
        private System.Data.DataTable GetDoctorListData()
        {
            string excelConnString = @"Data Source=DESKTOP-V217U6T;Initial Catalog=PatientFileBackup2;Integrated Security=True";

            SqlConnection excelLoadConn = new SqlConnection(excelConnString);

            System.Data.DataTable excelDataTable = new System.Data.DataTable();
            try
            {
                string adressQuery = @"SELECT [Идентификатор врача].[Идентификатор врача], 
[Учреждение здравоохранения].[Название учреждения], [Врач].[Фамилия врача], [Врач].[Имя врача],
[Врач].[Отчество врача], [Специализация врача].Специальность, [Идентификатор врача].Категория, Врач.[Рабочий телефон]
FROM [Учреждение здравоохранения] INNER JOIN ([Специализация врача] INNER JOIN (Врач INNER JOIN [Идентификатор врача] 
ON Врач.[Код врача] = [Идентификатор врача].[Код врача]) ON [Специализация врача].Код_специальности = [Идентификатор врача].[Код специальности])
ON [Учреждение здравоохранения].[Код учреждения] = Врач.[Код учреждения здравоохранения];
";
                SqlCommand adressLoadComm = new SqlCommand(adressQuery, excelLoadConn);

                excelLoadConn.Open();
                SqlDataAdapter adressDataAdapter = new SqlDataAdapter(adressLoadComm);
                DataSet adressDataSet = new DataSet();
                adressDataAdapter.Fill(adressDataSet);
                excelDataTable = adressDataSet.Tables[0];
            }
            catch (Exception)
            {
               // MessageBox.Show("Ошибка выполнения запроса! Обратитесь в техническую поддержку!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                excelLoadConn.Close();
                excelLoadConn.Dispose();
            }
            return excelDataTable;
        }
        #endregion

        #region
        public ActionResult ParentList()
        {
            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                excelApplication.Workbooks.Add(Type.Missing);

                excelApplication.Interactive = false;
                excelApplication.EnableEvents = false;

                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApplication.Sheets[1];

                excelSheet.Name = "Список представителей";

                System.Data.DataTable excelDataTable = GetRelativesList();

                int columnIndex = 0;
                int rowIndex = 0;
                string adressData = "";
                Microsoft.Office.Interop.Excel.Range excelSheeetRange;
                for (int i = 0; i < excelDataTable.Columns.Count; i++)
                {
                    adressData = excelDataTable.Columns[i].ColumnName.ToString();
                    excelSheet.Cells[1, i + 1] = adressData;
                    excelSheeetRange = excelSheet.get_Range("A1:Z1", Type.Missing);

                    excelSheeetRange.WrapText = true;
                    excelSheeetRange.Font.Bold = true;
                }
                for (rowIndex = 0; rowIndex < excelDataTable.Rows.Count; rowIndex++)
                {
                    for (columnIndex = 0; columnIndex < excelDataTable.Columns.Count; columnIndex++)
                    {
                        adressData = excelDataTable.Rows[rowIndex].ItemArray[columnIndex].ToString();
                        excelSheet.Cells[rowIndex + 2, columnIndex + 1] = adressData;
                    }
                }

                excelSheeetRange = excelSheet.UsedRange;
                excelSheeetRange.Columns.AutoFit();
                excelSheet.Rows.AutoFit();

            }
            catch (Exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Обратитесь в техническую поддержку!');</script>");
            }
            finally
            {
                excelApplication.Visible = true;

                excelApplication.Interactive = true;
                excelApplication.ScreenUpdating = true;
                excelApplication.UserControl = true;
            }
            return RedirectToAction("Index");
        }
        private System.Data.DataTable GetRelativesList()
        {
            string excelConnString = @"Data Source=DESKTOP-V217U6T;Initial Catalog=PatientFileBackup2;Integrated Security=True";

            SqlConnection excelLoadConn = new SqlConnection(excelConnString);

            System.Data.DataTable excelDataTable = new System.Data.DataTable();
            try
            {
                string adressQuery = @"SELECT [Код_представителя]
      ,[Фамилия представителя]
      ,[Имя представителя]
      ,[Отчество представителя]
      ,[Серия и номер паспорта]
      ,[Место работы]
      ,[Контактный телефон представителя]
  FROM [dbo].[Карточка представителя]
";
                SqlCommand adressLoadComm = new SqlCommand(adressQuery, excelLoadConn);

                excelLoadConn.Open();
                SqlDataAdapter adressDataAdapter = new SqlDataAdapter(adressLoadComm);
                DataSet adressDataSet = new DataSet();
                adressDataAdapter.Fill(adressDataSet);
                excelDataTable = adressDataSet.Tables[0];
            }
            catch (Exception)
            {
             //   MessageBox.Show(ex.Message, "Возникла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                excelLoadConn.Close();
                excelLoadConn.Dispose();
            }
            return excelDataTable;
        }
        #endregion

        #region
        public ActionResult AdressList()
        {
            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                excelApplication.Workbooks.Add(Type.Missing);
                excelApplication.Interactive = false;
                excelApplication.EnableEvents = false;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApplication.Sheets[1];

                excelSheet.Name = "Список адресов пациента";

                System.Data.DataTable excelDataTable = GetAdressListData();

                int columnIndex = 0;
                int rowIndex = 0;
                string adressData = "";
                Microsoft.Office.Interop.Excel.Range excelSheeetRange;
                for (int i = 0; i < excelDataTable.Columns.Count; i++)
                {
                    adressData = excelDataTable.Columns[i].ColumnName.ToString();
                    excelSheet.Cells[1, i + 1] = adressData;

                    excelSheeetRange = excelSheet.get_Range("A1:Z1", Type.Missing);

                    excelSheeetRange.WrapText = true;
                    excelSheeetRange.Font.Bold = true;
                }

                for (rowIndex = 0; rowIndex < excelDataTable.Rows.Count; rowIndex++)
                {
                    for (columnIndex = 0; columnIndex < excelDataTable.Columns.Count; columnIndex++)
                    {
                        adressData = excelDataTable.Rows[rowIndex].ItemArray[columnIndex].ToString();
                        excelSheet.Cells[rowIndex + 2, columnIndex + 1] = adressData;
                    }
                }

                excelSheeetRange = excelSheet.UsedRange;

                excelSheeetRange.Columns.AutoFit();
                excelSheet.Rows.AutoFit();

            }
            catch (Exception )
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Oбратитесь в техническую поддержку!');</script>");
            }
            finally
            {
                excelApplication.Visible = true;

                excelApplication.Interactive = true;
                excelApplication.ScreenUpdating = true;
                excelApplication.UserControl = true;
            }
            return RedirectToAction("Index");
        }

        private System.Data.DataTable GetAdressListData()
        {
            string excelConnString = @"Data Source=DESKTOP-V217U6T;Initial Catalog=PatientFileBackup2;Integrated Security=True";

            SqlConnection excelLoadConn = new SqlConnection(excelConnString);

            System.Data.DataTable excelDataTable = new System.Data.DataTable();
            try
            {
                string adressQuery = @"SELECT [Адрес пациента].[Код адреса], [Электронная амбулаторная карта].[Номер амбулаторной карты], [Электронная амбулаторная карта].Фамилия, [Электронная амбулаторная карта].Имя, [Электронная амбулаторная карта].Отчество, [Адрес пациента].город, [Адрес пациента].улица, [Адрес пациента].Дом, [Адрес пациента].[Квартира/комната]
FROM [Электронная амбулаторная карта] INNER JOIN [Адрес пациента] ON [Электронная амбулаторная карта].[Номер амбулаторной карты] = [Адрес пациента].[Номер амбулаторной карты];
";
                SqlCommand adressLoadComm = new SqlCommand(adressQuery, excelLoadConn);

                excelLoadConn.Open();
                SqlDataAdapter adressDataAdapter = new SqlDataAdapter(adressLoadComm);
                DataSet adressDataSet = new DataSet();
                adressDataAdapter.Fill(adressDataSet);
                excelDataTable = adressDataSet.Tables[0];
            }
            catch (Exception )
            {
                //MessageBox.Show("Ошибка выполнения запроса! Обратитесь к техническую поддержку!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                excelLoadConn.Close();
                excelLoadConn.Dispose();
            }
            return excelDataTable;
        }
        #endregion

        #region
        public ActionResult RelationList()
        {
            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                //добавляем книгу
                excelApplication.Workbooks.Add(Type.Missing);

                //делаем временно неактивным документ
                excelApplication.Interactive = false;
                excelApplication.EnableEvents = false;

                //выбираем лист на котором будем работать (Лист 1)
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApplication.Sheets[1];
                //Название листа
                excelSheet.Name = "Список родственнных связей";

                //Выгрузка данных из таблицы данных, заполненной c помощью sql-запроса
                System.Data.DataTable excelDataTable = GetRelationList();

                int columnIndex = 0;
                int rowIndex = 0;
                string adressData = "";
                Microsoft.Office.Interop.Excel.Range excelSheeetRange;
                //заполнение колонок листа excel
                for (int i = 0; i < excelDataTable.Columns.Count; i++)
                {
                    adressData = excelDataTable.Columns[i].ColumnName.ToString();
                    excelSheet.Cells[1, i + 1] = adressData;

                    //определяется диапазон ячеек
                    excelSheeetRange = excelSheet.get_Range("A1:Z1", Type.Missing);

                    excelSheeetRange.WrapText = true;
                    excelSheeetRange.Font.Bold = true;
                }

                //заполняем строки
                for (rowIndex = 0; rowIndex < excelDataTable.Rows.Count; rowIndex++)
                {
                    for (columnIndex = 0; columnIndex < excelDataTable.Columns.Count; columnIndex++)
                    {
                        adressData = excelDataTable.Rows[rowIndex].ItemArray[columnIndex].ToString();
                        excelSheet.Cells[rowIndex + 2, columnIndex + 1] = adressData;
                    }
                }

                //выбираем всю область данных
                excelSheeetRange = excelSheet.UsedRange;

                //выравниваем строки и колонки по их содержимому
                excelSheeetRange.Columns.AutoFit();
                excelSheet.Rows.AutoFit();

            }
            catch (Exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Oбратитесь в техническую поддержку!');</script>");
            }
            finally
            {
                //Показываем ексель
                excelApplication.Visible = true;

                excelApplication.Interactive = true;
                excelApplication.ScreenUpdating = true;
                excelApplication.UserControl = true;
            }
            return RedirectToAction("Index");
        }
        private System.Data.DataTable GetRelationList()
        {
            string excelConnString = @"Data Source=DESKTOP-V217U6T;Initial Catalog=PatientFileBackup2;Integrated Security=True";

            SqlConnection excelLoadConn = new SqlConnection(excelConnString);

            System.Data.DataTable excelDataTable = new System.Data.DataTable();
            try
            {
                string adressQuery = @"SELECT [Связь пациента с представителем].[Код родственной связи], [Электронная амбулаторная карта].Фамилия, [Электронная амбулаторная карта].Имя, [Электронная амбулаторная карта].Отчество, [Карточка представителя].[Фамилия представителя], [Карточка представителя].[Имя представителя], [Карточка представителя].[Отчество представителя], [Родственная связь].[Название родственной связи]
FROM [Карточка представителя] INNER JOIN ([Электронная амбулаторная карта] INNER JOIN ([Родственная связь] INNER JOIN [Связь пациента с представителем] ON [Родственная связь].[Код родства, свойства] = [Связь пациента с представителем].[Родственная связь]) ON [Электронная амбулаторная карта].[Номер амбулаторной карты] = [Связь пациента с представителем].[Номер амбулаторной карты пациента]) ON [Карточка представителя].Код_представителя = [Связь пациента с представителем].[Код представителя];

";
                SqlCommand adressLoadComm = new SqlCommand(adressQuery, excelLoadConn);

                excelLoadConn.Open();
                SqlDataAdapter adressDataAdapter = new SqlDataAdapter(adressLoadComm);
                DataSet adressDataSet = new DataSet();
                adressDataAdapter.Fill(adressDataSet);
                excelDataTable = adressDataSet.Tables[0];
            }
            catch (Exception)
            {
             //   MessageBox.Show(ex.Message, "Возникла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                excelLoadConn.Close();
                excelLoadConn.Dispose();
            }
            return excelDataTable;
        }

        #endregion

        #region
        public ActionResult CheckupList()
        {
            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                //добавляем книгу
                excelApplication.Workbooks.Add(Type.Missing);

                //делаем временно неактивным документ
                excelApplication.Interactive = false;
                excelApplication.EnableEvents = false;

                //выбираем лист на котором будем работать (Лист 1)
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApplication.Sheets[1];
                //Название листа
                excelSheet.Name = "Список осмотров";

                //Выгрузка данных из таблицы данных, заполненной c помощью sql-запроса
                System.Data.DataTable excelDataTable = GetCheckupList();

                int columnIndex = 0;
                int rowIndex = 0;
                string adressData = "";
                Microsoft.Office.Interop.Excel.Range excelSheeetRange;
                //заполнение колонок листа excel
                for (int i = 0; i < excelDataTable.Columns.Count; i++)
                {
                    adressData = excelDataTable.Columns[i].ColumnName.ToString();
                    excelSheet.Cells[1, i + 1] = adressData;

                    //определяется диапазон ячеек
                    excelSheeetRange = excelSheet.get_Range("A1:Z1", Type.Missing);

                    excelSheeetRange.WrapText = true;
                    excelSheeetRange.Font.Bold = true;
                }

                //заполняем строки
                for (rowIndex = 0; rowIndex < excelDataTable.Rows.Count; rowIndex++)
                {
                    for (columnIndex = 0; columnIndex < excelDataTable.Columns.Count; columnIndex++)
                    {
                        adressData = excelDataTable.Rows[rowIndex].ItemArray[columnIndex].ToString();
                        excelSheet.Cells[rowIndex + 2, columnIndex + 1] = adressData;
                    }
                }

                //выбираем всю область данных
                excelSheeetRange = excelSheet.UsedRange;

                //выравниваем строки и колонки по их содержимому
                excelSheeetRange.Columns.AutoFit();
                excelSheet.Rows.AutoFit();

            }
            catch (Exception )
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Oбратитесь в техническую поддержку!');</script>");
            }
            finally
            {
                //Показываем ексель
                excelApplication.Visible = true;

                excelApplication.Interactive = true;
                excelApplication.ScreenUpdating = true;
                excelApplication.UserControl = true;
            }
            return RedirectToAction("Index");
        }

        private System.Data.DataTable GetCheckupList()
        {
            string excelConnString = @"Data Source=DESKTOP-V217U6T;Initial Catalog=PatientFileBackup2;Integrated Security=True";

            SqlConnection excelLoadConn = new SqlConnection(excelConnString);

            System.Data.DataTable excelDataTable = new System.Data.DataTable();
            try
            {
                string adressQuery = @"SELECT DISTINCT [Осмотр пациента].[Номер осмотра],
[Электронная амбулаторная карта].Фамилия,
[Электронная амбулаторная карта].Имя, [Электронная амбулаторная карта].Отчество, [Электронная амбулаторная карта].[Дата рождения],
[Электронная амбулаторная карта].Пол, [Врач].[Фамилия врача], [Врач].[Имя врача],
[Врач].[Отчество врача], [Специализация врача].Специальность, [Осмотр пациента].[Дата приема], 
[Осмотр пациента].[Цель посещения], [Осмотр пациента].[Повторный прием], [Диагнозы за осмотр].[Код диагноза], 
[Назначения препаратов].[Назначение по АТХ]

FROM [Электронная амбулаторная карта] INNER JOIN 
(([Специализация врача] INNER JOIN (([Идентификатор врача] INNER JOIN [Осмотр пациента] ON
[Идентификатор врача].[Идентификатор врача] = [Осмотр пациента].[Идентификатор врача] inner join Врач on [Идентификатор врача].[Код врача]=Врач.[Код врача]) INNER JOIN
[Диагнозы за осмотр] ON [Осмотр пациента].[Номер осмотра] = [Диагнозы за осмотр].[Номер осмотра]) ON
[Специализация врача].Код_специальности = [Идентификатор врача].[Код специальности]) INNER JOIN [Назначения препаратов]
ON [Осмотр пациента].[Номер осмотра] = [Назначения препаратов].[Номер осмотра]) ON
[Электронная амбулаторная карта].[Номер амбулаторной карты] = [Осмотр пациента].[Номер амбулаторной карты];

";
                SqlCommand adressLoadComm = new SqlCommand(adressQuery, excelLoadConn);

                excelLoadConn.Open();
                SqlDataAdapter adressDataAdapter = new SqlDataAdapter(adressLoadComm);
                DataSet adressDataSet = new DataSet();
                adressDataAdapter.Fill(adressDataSet);
                excelDataTable = adressDataSet.Tables[0];
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message, "Возникла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                excelLoadConn.Close();
                excelLoadConn.Dispose();
            }
            return excelDataTable;
        }

        #endregion

        #region
        public ActionResult CheckupAmount()
        {
            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                //добавляем книгу
                excelApplication.Workbooks.Add(Type.Missing);

                //делаем временно неактивным документ
                excelApplication.Interactive = false;
                excelApplication.EnableEvents = false;

                //выбираем лист на котором будем работать (Лист 1)
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApplication.Sheets[1];
                //Название листа
                excelSheet.Name = "Количество осмотров врачей";

                //Выгрузка данных из таблицы данных, заполненной c помощью sql-запроса
                System.Data.DataTable excelDataTable = NumberOfCheckups();

                int columnIndex = 0;
                int rowIndex = 0;
                string adressData = "";
                Microsoft.Office.Interop.Excel.Range excelSheeetRange;
                //заполнение колонок листа excel
                for (int i = 0; i < excelDataTable.Columns.Count; i++)
                {
                    adressData = excelDataTable.Columns[i].ColumnName.ToString();
                    excelSheet.Cells[1, i + 1] = adressData;

                    //определяется диапазон ячеек
                    excelSheeetRange = excelSheet.get_Range("A1:Z1", Type.Missing);

                    excelSheeetRange.WrapText = true;
                    excelSheeetRange.Font.Bold = true;
                }

                //заполняем строки
                for (rowIndex = 0; rowIndex < excelDataTable.Rows.Count; rowIndex++)
                {
                    for (columnIndex = 0; columnIndex < excelDataTable.Columns.Count; columnIndex++)
                    {
                        adressData = excelDataTable.Rows[rowIndex].ItemArray[columnIndex].ToString();
                        excelSheet.Cells[rowIndex + 2, columnIndex + 1] = adressData;
                    }
                }

                excelSheeetRange = excelSheet.UsedRange;

                excelSheeetRange.Columns.AutoFit();
                //  excelSheet.Rows.AutoFit();

            }
            catch (Exception )
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Oбратитесь в техническую поддержку!');</script>");
            }
            finally
            {
                //Показываем ексель
                excelApplication.Visible = true;

                excelApplication.Interactive = true;
                excelApplication.ScreenUpdating = true;
                excelApplication.UserControl = true;
            }
            return RedirectToAction("Stats");
        }
        private System.Data.DataTable NumberOfCheckups()
        {
            string excelConnString = @"Data Source=DESKTOP-V217U6T;Initial Catalog=PatientFileBackup2;Integrated Security=True";

            SqlConnection excelLoadConn = new SqlConnection(excelConnString);

            System.Data.DataTable excelDataTable = new System.Data.DataTable();
            try
            {
                string adressQuery = @"SELECT [Идентификатор врача].[Идентификатор врача], Врач.[Фамилия врача], Врач.[Имя врача],
Врач.[Отчество врача], Count(*) AS [Количество осмотров проведенных врачом]
FROM [Идентификатор врача] INNER JOIN [Осмотр пациента] ON [Идентификатор врача].[Идентификатор врача] = [Осмотр пациента].[Идентификатор врача] inner join Врач on [Идентификатор врача].[Код врача]=Врач.[Код врача]
GROUP BY [Идентификатор врача].[Идентификатор врача], Врач.[Фамилия врача], Врач.[Имя врача], Врач.[Отчество врача]
ORDER BY Count(*) desc;

";
                SqlCommand adressLoadComm = new SqlCommand(adressQuery, excelLoadConn);

                excelLoadConn.Open();
                SqlDataAdapter adressDataAdapter = new SqlDataAdapter(adressLoadComm);
                DataSet adressDataSet = new DataSet();
                adressDataAdapter.Fill(adressDataSet);
                excelDataTable = adressDataSet.Tables[0];
            }
            catch (Exception )
            {
                //MessageBox.Show(ex.Message, "Возникла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                excelLoadConn.Close();
                excelLoadConn.Dispose();
            }
            return excelDataTable;
        }
        #endregion

        #region
        public ActionResult PatCheckupNumber()
        {
            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                //добавляем книгу
                excelApplication.Workbooks.Add(Type.Missing);

                //делаем временно неактивным документ
                excelApplication.Interactive = false;
                excelApplication.EnableEvents = false;

                //выбираем лист на котором будем работать (Лист 1)
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApplication.Sheets[1];
                //Название листа
                excelSheet.Name = "Количество осмотров пациента";

                //Выгрузка данных из таблицы данных, заполненной c помощью sql-запроса
                System.Data.DataTable excelDataTable = NumberOfPAtientCheckups();

                int columnIndex = 0;
                int rowIndex = 0;
                string adressData = "";
                Microsoft.Office.Interop.Excel.Range excelSheeetRange;
                //заполнение колонок листа excel
                for (int i = 0; i < excelDataTable.Columns.Count; i++)
                {
                    adressData = excelDataTable.Columns[i].ColumnName.ToString();
                    excelSheet.Cells[1, i + 1] = adressData;

                    //определяется диапазон ячеек
                    excelSheeetRange = excelSheet.get_Range("A1:Z1", Type.Missing);

                    excelSheeetRange.WrapText = true;
                    excelSheeetRange.Font.Bold = true;
                }

                //заполняем строки
                for (rowIndex = 0; rowIndex < excelDataTable.Rows.Count; rowIndex++)
                {
                    for (columnIndex = 0; columnIndex < excelDataTable.Columns.Count; columnIndex++)
                    {
                        adressData = excelDataTable.Rows[rowIndex].ItemArray[columnIndex].ToString();
                        excelSheet.Cells[rowIndex + 2, columnIndex + 1] = adressData;
                    }
                }

                excelSheeetRange = excelSheet.UsedRange;

                excelSheeetRange.Columns.AutoFit();
                excelSheeetRange.Rows.AutoFit();
            }
            catch (Exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Oбратитесь в техническую поддержку!');</script>");
            }
            finally
            {
                //Показываем ексель
                excelApplication.Visible = true;

                excelApplication.Interactive = true;
                excelApplication.ScreenUpdating = true;
                excelApplication.UserControl = true;
            }
            return RedirectToAction("Stats");
        }
        private System.Data.DataTable NumberOfPAtientCheckups()
        {
            string excelConnString = @"Data Source=DESKTOP-V217U6T;Initial Catalog=PatientFileBackup2;Integrated Security=True";

            SqlConnection excelLoadConn = new SqlConnection(excelConnString);

            System.Data.DataTable excelDataTable = new System.Data.DataTable();
            try
            {
                string adressQuery = @"SELECT [Электронная амбулаторная карта].[Номер амбулаторной карты], [Электронная амбулаторная карта].Фамилия,
[Электронная амбулаторная карта].Имя, [Электронная амбулаторная карта].Отчество, Count(*) AS [Количество осмотров у каждого пациента]
FROM [Электронная амбулаторная карта] INNER JOIN [Осмотр пациента] ON [Электронная амбулаторная карта].[Номер амбулаторной карты] = [Осмотр пациента].[Номер амбулаторной карты]
GROUP BY [Электронная амбулаторная карта].[Номер амбулаторной карты], [Электронная амбулаторная карта].Фамилия, [Электронная амбулаторная карта].Имя, [Электронная амбулаторная карта].Отчество
ORDER BY Count(*);


";
                SqlCommand adressLoadComm = new SqlCommand(adressQuery, excelLoadConn);

                excelLoadConn.Open();
                SqlDataAdapter adressDataAdapter = new SqlDataAdapter(adressLoadComm);
                DataSet adressDataSet = new DataSet();
                adressDataAdapter.Fill(adressDataSet);
                excelDataTable = adressDataSet.Tables[0];
            }
            catch (Exception)
            {
              //  MessageBox.Show(ex.Message, "Возникла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                excelLoadConn.Close();
                excelLoadConn.Dispose();
            }
            return excelDataTable;
        }
        #endregion

        #region
        public ActionResult IllnessCount()
        {
            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                //добавляем книгу
                excelApplication.Workbooks.Add(Type.Missing);

                //делаем временно неактивным документ
                excelApplication.Interactive = false;
                excelApplication.EnableEvents = false;

                //выбираем лист на котором будем работать (Лист 1)
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApplication.Sheets[1];
                //Название листа
                excelSheet.Name = "Самые частые диагнозы";

                //Выгрузка данных из таблицы данных, заполненной c помощью sql-запроса
                System.Data.DataTable excelDataTable = FrequentIllnesses();

                int columnIndex = 0;
                int rowIndex = 0;
                string adressData = "";
                Microsoft.Office.Interop.Excel.Range excelSheeetRange;
                //заполнение колонок листа excel
                for (int i = 0; i < excelDataTable.Columns.Count; i++)
                {
                    adressData = excelDataTable.Columns[i].ColumnName.ToString();
                    excelSheet.Cells[1, i + 1] = adressData;

                    //определяется диапазон ячеек
                    excelSheeetRange = excelSheet.get_Range("A1:Z1", Type.Missing);

                    excelSheeetRange.WrapText = true;
                    excelSheeetRange.Font.Bold = true;
                }

                //заполняем строки
                for (rowIndex = 0; rowIndex < excelDataTable.Rows.Count; rowIndex++)
                {
                    for (columnIndex = 0; columnIndex < excelDataTable.Columns.Count; columnIndex++)
                    {
                        adressData = excelDataTable.Rows[rowIndex].ItemArray[columnIndex].ToString();
                        excelSheet.Cells[rowIndex + 2, columnIndex + 1] = adressData;
                    }
                }

                excelSheeetRange = excelSheet.UsedRange;

                excelSheeetRange.Columns.AutoFit();
                excelSheet.Rows.AutoFit();

            }
            catch (Exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Oбратитесь в техническую поддержку!');</script>");
            }
            finally
            {
                //Показываем ексель
                excelApplication.Visible = true;

                excelApplication.Interactive = true;
                excelApplication.ScreenUpdating = true;
                excelApplication.UserControl = true;
            }
            return RedirectToAction("Stats");
        }
        private System.Data.DataTable FrequentIllnesses()
        {
            string excelConnString = @"Data Source=DESKTOP-V217U6T;Initial Catalog=PatientFileBackup2;Integrated Security=True";

            SqlConnection excelLoadConn = new SqlConnection(excelConnString);

            System.Data.DataTable excelDataTable = new System.Data.DataTable();
            try
            {
                string adressQuery = @"SELECT [Диагнозы за осмотр].[Диагноз по МКБ-10], Count(*) AS [Количество поставленных диагнозов]
FROM [Осмотр пациента] INNER JOIN [Диагнозы за осмотр] ON [Осмотр пациента].[Номер осмотра] = [Диагнозы за осмотр].[Номер осмотра]
GROUP BY [Диагнозы за осмотр].[Диагноз по МКБ-10]
ORDER BY Count(*) DESC;
";
                SqlCommand adressLoadComm = new SqlCommand(adressQuery, excelLoadConn);

                excelLoadConn.Open();
                SqlDataAdapter adressDataAdapter = new SqlDataAdapter(adressLoadComm);
                DataSet adressDataSet = new DataSet();
                adressDataAdapter.Fill(adressDataSet);
                excelDataTable = adressDataSet.Tables[0];
            }
            catch (Exception )
            {
              //  MessageBox.Show(ex.Message, "Возникла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                excelLoadConn.Close();
                excelLoadConn.Dispose();
            }
            return excelDataTable;
        }

        #endregion

        #region
        public ActionResult MedsCount()
        {
            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                //добавляем книгу
                excelApplication.Workbooks.Add(Type.Missing);

                //делаем временно неактивным документ
                excelApplication.Interactive = false;
                excelApplication.EnableEvents = false;

                //выбираем лист на котором будем работать (Лист 1)
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApplication.Sheets[1];
                //Название листа
                excelSheet.Name = "Самые назначаемые препараты";

                //Выгрузка данных из таблицы данных, заполненной c помощью sql-запроса
                System.Data.DataTable excelDataTable = FrequentMeds();

                int columnIndex = 0;
                int rowIndex = 0;
                string adressData = "";
                Microsoft.Office.Interop.Excel.Range excelSheeetRange;
                //заполнение колонок листа excel
                for (int i = 0; i < excelDataTable.Columns.Count; i++)
                {
                    adressData = excelDataTable.Columns[i].ColumnName.ToString();
                    excelSheet.Cells[1, i + 1] = adressData;

                    //определяется диапазон ячеек
                    excelSheeetRange = excelSheet.get_Range("A1:Z1", Type.Missing);

                    excelSheeetRange.WrapText = true;
                    excelSheeetRange.Font.Bold = true;
                }

                //заполняем строки
                for (rowIndex = 0; rowIndex < excelDataTable.Rows.Count; rowIndex++)
                {
                    for (columnIndex = 0; columnIndex < excelDataTable.Columns.Count; columnIndex++)
                    {
                        adressData = excelDataTable.Rows[rowIndex].ItemArray[columnIndex].ToString();
                        excelSheet.Cells[rowIndex + 2, columnIndex + 1] = adressData;
                    }
                }

                excelSheeetRange = excelSheet.UsedRange;

                excelSheeetRange.Columns.AutoFit();
                excelSheet.Rows.AutoFit();

            }
            catch (Exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Oбратитесь в техническую поддержку!');</script>");
            }
            finally
            {
                //Показываем ексель
                excelApplication.Visible = true;

                excelApplication.Interactive = true;
                excelApplication.ScreenUpdating = true;
                excelApplication.UserControl = true;
            }
            return RedirectToAction("Stats");
        }
        private System.Data.DataTable FrequentMeds()
        {
            string excelConnString = @"Data Source=DESKTOP-V217U6T;Initial Catalog=PatientFileBackup2;Integrated Security=True";

            SqlConnection excelLoadConn = new SqlConnection(excelConnString);

            System.Data.DataTable excelDataTable = new System.Data.DataTable();
            try
            {
                string adressQuery = @"select [Назначение по АТХ], count (*) as [Количество выписанных препаратов]
from [Осмотр пациента] as checkup
inner join [Назначения препаратов] as meds
on checkup.[Номер осмотра]=meds.[Номер осмотра]
where meds.[Назначение по АТХ] is not null and meds.[Назначение по АТХ]!=''
group by meds.[Назначение по АТХ]
order by count (*) desc
";
                SqlCommand adressLoadComm = new SqlCommand(adressQuery, excelLoadConn);

                excelLoadConn.Open();
                SqlDataAdapter adressDataAdapter = new SqlDataAdapter(adressLoadComm);
                DataSet adressDataSet = new DataSet();
                adressDataAdapter.Fill(adressDataSet);
                excelDataTable = adressDataSet.Tables[0];
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message, "Возникла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                excelLoadConn.Close();
                excelLoadConn.Dispose();
            }
            return excelDataTable;
        }
        #endregion
    }
}