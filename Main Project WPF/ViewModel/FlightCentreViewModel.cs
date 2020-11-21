
using Main_Project;
using Main_Project.Exceptions;
using Main_Project.Facade;
using Main_Project.POCO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Main_Project_WPF.ViewModel
{
    public class FlightCentreViewModel : INotifyPropertyChanged
    {
        private string _info;

        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                OnPropertyChanged("Info");
            }
        }

        private int _progressBarValue;
        public int ProgressBarValue
        {
            get { return _progressBarValue; }
            set
            {
                _progressBarValue = value;
                OnPropertyChanged("ProgressBarValue");
            }
        }

        private string _progressBarValueInfo;
        public string ProgressBarValueInfo
        {
            get { return _progressBarValueInfo + "%" + " complete"; }
            set
            {
                _progressBarValueInfo = value;
                OnPropertyChanged("ProgressBarValueInfo");
            }
        }

        private bool _workNotInProgress;
        public bool WorkNotInProgress
        {
            get { return _workNotInProgress; }
            set
            {
                _workNotInProgress = value;
                OnPropertyChanged("WorkNotInProgress");
            }
        }

        public WpfFacade wpfFacade = new WpfFacade();
        public string Administrator { get; set; }
        public string Countries { get; set; }
        public string AirlineCompany { get; set; }
        public string Customer { get; set; }
        public string Flights { get; set; }
        public string Ticket { get; set; }

        #region DelegateCommand ReplaceButtonCommand
        public DelegateCommand ReplaceButtonCommand { get; set; }
        /// <summary>
        /// Checks if the code can be executed
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteMethodReplace()
        {
            if (IsTextBoxContainNotANumber() && WorkNotInProgress)
                return true;

            return false;
        }
        /// <summary>
        /// executing the code
        /// </summary>
        private void ExecuteCommandReplace()
        {
            RemoveAllDataBaseInfo();
            ExecuteDbUpdate();
        }

        #endregion

        #region DelegateCommand AddtoDbButtonCommand
        public DelegateCommand AddtoDbButtonCommand { get; set; }

        /// <summary>
        /// Checks if the code can be executed
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteMethodAdd()
        {
            if (IsTextBoxContainNotANumber() && WorkNotInProgress)
                return true;

            return false;
        }

        /// <summary>
        /// executing the code
        /// </summary>
        private void ExecuteCommandAdd()
        {
            ExecuteDbUpdate();
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public FlightCentreViewModel()
        {
            Administrator = "1";
            Countries = "8";
            AirlineCompany = "5";
            Customer = "30";
            Flights = "25";
            Ticket = "10";
            Info = string.Empty;
            _workNotInProgress = true;
            ProgressBarValue = 0;
            ProgressBarValueInfo = ProgressBarValue.ToString();
            ReplaceButtonCommand = new DelegateCommand(ExecuteCommandReplace, CanExecuteMethodReplace);
            AddtoDbButtonCommand = new DelegateCommand(ExecuteCommandAdd, CanExecuteMethodAdd);

            Task.Run(() =>
            {
                while (true)
                {
                    ReplaceButtonCommand.RaiseCanExecuteChanged();
                    AddtoDbButtonCommand.RaiseCanExecuteChanged();
                    Thread.Sleep(300);
                }
            });
        }
        #endregion

        #region Remove all dataBase info
        /// <summary>
        /// Remove all dataBase info
        /// </summary>
        private void RemoveAllDataBaseInfo()
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REMOVE_ALL_INFO_FROM_DATA_BASE", conn);

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
        private void ExecuteDbUpdate()
        {
            // in order to draw the progress bar use Dispatcher...
            Info = string.Empty;
            bool succeed = true;
            int number_of_items = (Convert.ToInt32(Administrator)) + (Convert.ToInt32(Countries)) + (Convert.ToInt32(AirlineCompany)) + (Convert.ToInt32(Customer)) + (Convert.ToInt32(Flights)) * (Convert.ToInt32(AirlineCompany)) + (Convert.ToInt32(Ticket)) * (Convert.ToInt32(Customer));
            int current = 0;
            WorkNotInProgress = false;
            Task.Run(() =>
            {
                Random rnd = new Random();
                bool trynumber = int.TryParse(Countries, out int countries); // 3 or more countries doesn't work help
                for (int i = 0; i < countries; i++)
                {
                    do
                    {
                        try
                        {
                            wpfFacade.CreateNewCountry(DataGeneratorService.GenerateRandomCountry());
                            succeed = true;
                        }
                        catch (UserNameAlreadyExistsException)
                        {
                            succeed = false;
                        }
                    }
                    while (!succeed);
                    current++;
                    ProgressBarValue = (int)(((double)current / number_of_items) * 100);
                    ProgressBarValueInfo = ProgressBarValue.ToString();
                }
                Info = $"Added {countries} countries to the dataBase \n";
                // countries

                trynumber = int.TryParse(Administrator, out int administrator);
                for (int i = 0; i < administrator; i++)
                {
                    do
                    {
                        try
                        {
                            wpfFacade.CreateNewAdmin(DataGeneratorService.GenerateRandomUserRepositoryForAdmin(), DataGeneratorService.GenerateRandomAdmin());
                            succeed = true;
                        }
                        catch (UserNameAlreadyExistsException)
                        {
                            succeed = false;
                        }
                    }
                    while (!succeed);
                    current++;
                    ProgressBarValue = (int)(((double)current / number_of_items) * 100);
                    ProgressBarValueInfo = ProgressBarValue.ToString();
                }
                Info += $"Added {administrator} administrator to the dataBase \n";
                // admins

                List<Country> listCountries = (List<Country>)wpfFacade.GetAllCountries();
                trynumber = int.TryParse(AirlineCompany, out int airlineCompany);
                for (int i = 0; i < airlineCompany; i++)
                {
                    do
                    {
                        try
                        {
                            int country = rnd.Next(0, listCountries.Count());
                            wpfFacade.CreateNewAirline(DataGeneratorService.GenerateRandomUserRepositoryForAirlineCompany(), DataGeneratorService.GenerateRandomAirlineCompany(), listCountries[country]);
                            succeed = true;
                        }
                        catch (UserNameAlreadyExistsException)
                        {
                            succeed = false;
                        }
                    }
                    while (!succeed);
                    current++;
                    ProgressBarValue = (int)(((double)current / number_of_items) * 100);
                    ProgressBarValueInfo = ProgressBarValue.ToString();
                }
                Info += $"Added {airlineCompany} airlineCompanies to the dataBase \n";
                // airlines

                List<AirlineCompany> listAirlineCompanies = (List<AirlineCompany>)wpfFacade.GetAllAirlineCompanies();
                trynumber = int.TryParse(Flights, out int flights);
                for (int i = 0; i < listAirlineCompanies.Count; i++)
                {
                    for (int j = 0; j < flights; j++)
                    {
                        int country = rnd.Next(0, listCountries.Count());
                        Flight flight = DataGeneratorService.GenerateRandomFlight();
                        flight.DestinationCountryCode = listCountries[country].ID;
                        country = rnd.Next(0, listCountries.Count());
                        flight.OriginCountryCode = listCountries[country].ID;
                        wpfFacade.CreateFlight(listAirlineCompanies[i], flight);
                        current++;
                        ProgressBarValue = (int)(((double)current / number_of_items) * 100);
                        ProgressBarValueInfo = ProgressBarValue.ToString();
                    }

                }
                Info += $"Added {flights} flights to every airline in the dataBase \n";
                // flights

                trynumber = int.TryParse(Customer, out int customers);
                for (int i = 0; i < customers; i++)
                {
                    do
                    {
                        try
                        {
                            wpfFacade.CreateCustomerAndUserRepository(DataGeneratorService.GenerateRandomUserRepositoryForCustomer(), DataGeneratorService.GenerateRandomCustomer());// help
                            succeed = true;
                        }
                        catch (UserNameAlreadyExistsException)
                        {
                            succeed = false;
                        }
                    }
                    while (!succeed);
                    current++;
                    ProgressBarValue = (int)(((double)current / number_of_items) * 100);
                    ProgressBarValueInfo = ProgressBarValue.ToString();
                }
                Info += $"Added {customers} customers to the dataBase \n";
                // customers

                List<Flight> listFlights = (List<Flight>)wpfFacade.GetAllFlights();
                List<Customer> listCustomers = (List<Customer>)wpfFacade.GetAllCustomers();
                trynumber = int.TryParse(Ticket, out int ticket);
                for (int i = 0; i < listCustomers.Count; i++)
                {
                    for (int j = 0; j < ticket; j++)
                    {
                        int flight = rnd.Next(0, listFlights.Count());
                        wpfFacade.PurchaseTicket(listCustomers[i], listFlights[flight]);
                        current++;
                        ProgressBarValue = (int)(((double)current / number_of_items) * 100);
                        ProgressBarValueInfo = ProgressBarValue.ToString();
                    }
                   
                    
                }
                Info += $"Added {ticket} tickets to every customer in the dataBase \n";
                WorkNotInProgress = true;
            }
            );
            // tickets
            
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public bool IsTextBoxContainNotANumber()
        {
            List<string> valueList = new List<string>();
            valueList.Add(Administrator);
            valueList.Add(Countries);
            valueList.Add(AirlineCompany);
            valueList.Add(Flights);
            valueList.Add(Customer);
            valueList.Add(Customer);
            valueList.Add(Ticket);
            foreach (string str in valueList)
            {
                bool number = int.TryParse(str, out int num);
                if (!number || num<=0)
                    return false;
            }
            return true;

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
