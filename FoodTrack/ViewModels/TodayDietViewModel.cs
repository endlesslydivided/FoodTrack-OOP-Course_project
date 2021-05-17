﻿using FoodTrack.Context.UnitOfWork;
using FoodTrack.Commands;
using FoodTrack.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;
using FoodTrack.Views.Windows;

namespace FoodTrack.ViewModels
{
    public class TodayDietViewModel : BaseViewModel
    {
        private IEnumerable<Report> tableToShow;
        private DateTime dateToChoose;
        private Report lastSelected;
        public ICommand UpdateViewCommand { get; set; }

        public TodayDietViewModel()
        {
            DateToChoose = DateTime.Today;
            tableChangeToBreakfast();
        }

        #region Properties

       

        public string LastSelectedTable { get; set; }

        public Report LastSelected
        {
            get { return lastSelected; }
            set
            {
                lastSelected = value;
                OnPropertyChanged("LastSelected");
            }
        }

        public IEnumerable<Report> TableToShow
        {
            get { return tableToShow; }
            set
            {
                tableToShow = value;
                OnPropertyChanged("TableToShow");
            }
        }

        public DateTime DateToChoose
        {
            get { return dateToChoose; }
            set
            {
                dateToChoose = value;
                OnPropertyChanged("DateToChoose");
                refreshTable();
            }
        }


        #endregion

        #region Commands

        #region Изменить таблицу "завтрак"

        private DelegateCommand tableChangeToBreakfastCommand;

        public ICommand TableChangeToBreakfastCommand
        {
            get
            {
                if (tableChangeToBreakfastCommand == null)
                {
                    tableChangeToBreakfastCommand = new DelegateCommand(tableChangeToBreakfast);
                }
                return tableChangeToBreakfastCommand;
            }
        }

        private void tableChangeToBreakfast()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                User foundUser = unit.UserRepository.Get(x => x.UserLogin == deserializedUser.UserLogin).First<User>();
                IEnumerable<Report> reports = unit.ReportRepository.Get(x => x.ReportDate.Date.Equals(DateToChoose.Date) && x.IdReport == foundUser.Id && x.EatPeriod == "Завтрак");
                TableToShow = reports;
                LastSelectedTable = "Завтрак";
            }

        }

        #endregion
        #region Изменить таблицу "ланч"

        private DelegateCommand tableChangeToLunchCommand;

        public ICommand TableChangeToLunchCommand
        {
            get
            {
                if (tableChangeToLunchCommand == null)
                {
                    tableChangeToLunchCommand = new DelegateCommand(tableChangeToLunch);
                }
                return tableChangeToLunchCommand;
            }
        }

        private void tableChangeToLunch()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                User foundUser = unit.UserRepository.Get(x => x.UserLogin == deserializedUser.UserLogin).First<User>();
                IEnumerable<Report> reports = unit.ReportRepository.Get(x => x.ReportDate.Date.Equals(DateToChoose.Date) && x.IdReport == foundUser.Id && x.EatPeriod == "Ланч");
                TableToShow = reports;
                LastSelectedTable = "Ланч";
            }
        }

        #endregion
        #region Изменить таблицу "обед"

        private DelegateCommand tableChangeToDinnerCommand;

        public ICommand TableChangeToDinnerCommand
        {
            get
            {
                if (tableChangeToDinnerCommand == null)
                {
                    tableChangeToDinnerCommand = new DelegateCommand(tableChangeToDinner);
                }
                return tableChangeToDinnerCommand;
            }
        }

        private void tableChangeToDinner()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                User foundUser = unit.UserRepository.Get(x => x.UserLogin == deserializedUser.UserLogin).First<User>();
                IEnumerable<Report> reports = unit.ReportRepository.Get(x => x.ReportDate.Date.Equals(DateToChoose.Date) && x.IdReport == foundUser.Id && x.EatPeriod == "Обед");
                TableToShow = reports;
                LastSelectedTable = "Обед";
            }

        }

        #endregion
        #region Изменить таблицу "полдник"

        private DelegateCommand tableChangeToSnackCommand;

        public ICommand TableChangeToSnackCommand
        {
            get
            {
                if (tableChangeToSnackCommand == null)
                {
                    tableChangeToSnackCommand = new DelegateCommand(tableChangeToSnack);
                }
                return tableChangeToSnackCommand;
            }
        }

        private void tableChangeToSnack()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                User foundUser = unit.UserRepository.Get(x => x.UserLogin == deserializedUser.UserLogin).First<User>();
                IEnumerable<Report> reports = unit.ReportRepository.Get(x => x.ReportDate.Date.Equals(DateToChoose.Date) && x.IdReport == foundUser.Id && x.EatPeriod == "Полдник");
                TableToShow = reports;
                LastSelectedTable = "Полдинк";
            }

        }

        #endregion
        #region Изменить таблицу "ужин"

        private DelegateCommand tableChangeToSupperCommand;

        public ICommand TableChangeToSupperCommand
        {
            get
            {
                if (tableChangeToSupperCommand == null)
                {
                    tableChangeToSupperCommand = new DelegateCommand(tableChangeToSupper);
                }
                return tableChangeToSupperCommand;
            }
        }

        private void tableChangeToSupper()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                User foundUser = unit.UserRepository.Get(x => x.UserLogin == deserializedUser.UserLogin).First<User>();
                IEnumerable<Report> reports = unit.ReportRepository.Get(x => x.ReportDate.Date.Equals(DateToChoose.Date) && x.IdReport == foundUser.Id && x.EatPeriod == "Ужин");
                TableToShow = reports;
                LastSelectedTable = "Ужин";
            }
        }

        #endregion

        #region Увеличить дату на день

        private DelegateCommand addDayCommand;

        public ICommand AddDayCommand
        {
            get
            {
                if (addDayCommand == null)
                {
                    addDayCommand = new DelegateCommand(addDay);
                }
                return addDayCommand;
            }
        }

        private void addDay()
        {
            DateToChoose = DateToChoose.AddDays(1);
        }

        #endregion
        #region Уменьшить дату на день

        private DelegateCommand removeDayCommand;

        public ICommand RemoveDayCommand
        {
            get
            {
                if (removeDayCommand == null)
                {
                    removeDayCommand = new DelegateCommand(removeDay);
                }
                return removeDayCommand;
            }
        }

        private void removeDay()
        {
            DateToChoose = DateToChoose.AddDays(-1);
        }

        #endregion

        #region Удалить строку таблицы

        private DelegateCommand removeRowCommand;

        public ICommand RemoveRowCommand
        {
            get
            {
                if (removeRowCommand == null)
                {
                    removeRowCommand = new DelegateCommand(removeRow);
                }
                return removeRowCommand;
            }
        }

        private void removeRow()
        {
            if (LastSelected != null)
            {
                using (UnitOfWork unit = new UnitOfWork())
                {
                    unit.ReportRepository.Remove(LastSelected);
                    unit.Save();
                }
                refreshTable();
            }
        }

        #endregion

        #region Открыть окно изменения продукта

        private DelegateCommand openChangeProductWindowCommand;

        public ICommand OpenChangeProductWindowCommand
        {
            get
            {
                if (openChangeProductWindowCommand == null)
                {
                    openChangeProductWindowCommand = new DelegateCommand(openChangeProductWindow,canOpenChangeProductWindow);
                }
                return openChangeProductWindowCommand;
            }
        }

        private void openChangeProductWindow()
        {
            ChangeProductModalWindow window = new(LastSelected);
            if(window.ShowDialog() == true)
            {
                refreshTable();
            }

        }

        private bool canOpenChangeProductWindow()
        {
            if (LastSelected == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #endregion

        #region Открытые методы

        public void refreshTable()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                User foundUser = unit.UserRepository.Get(x => x.UserLogin == deserializedUser.UserLogin).First<User>();
                IEnumerable<Report> reports = unit.ReportRepository.Get(x => x.ReportDate.Date.Equals(DateToChoose.Date) && x.IdReport == foundUser.Id && x.EatPeriod == LastSelectedTable);              
                TableToShow = reports;
            }
        }
        #endregion

    }
}
