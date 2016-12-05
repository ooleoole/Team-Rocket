﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.App.Extensions;
using FootballManager.App.Utility;
using FootballManager.App.View;
using Dragablz;

namespace FootballManager.App.ViewModel
{
    public class TeamViewModel : ViewModelBase
    {
        private ObservableCollection<Team> teams;
        private ObservableCollection<TeamStats> teamStats;
        private TeamService teamService;
        private Team selectedTeam;
        private ICommand teamInfoCommand;
        private string teamViewSearchText;
        private string teamInfoSearchText;

        public TeamViewModel()
        {
            this.teamService = new TeamService();

            this.teamViewSearchText = "";
            this.teamInfoSearchText = "";
            this.teamStats = new ObservableCollection<TeamStats>();
            this.OpenTeamAddViewCommand = new RelayCommand(this.OpenTeamAddView);
            this.DeleteTeamCommand = new RelayCommand(this.DeleteTeam);
            this.LoadData();

            Messenger.Default.Register<Team>(this, this.OnTeamObjReceived);
        }

        #region Properties

        public ICommand OpenTeamAddViewCommand { get; }
        public ICommand DeleteTeamCommand { get; }

        public ICommand TeamInfoCommand
        {
            get
            {
                if (this.teamInfoCommand == null)
                {
                    this.teamInfoCommand = new RelayCommand(this.TeamInfo);
                }
                return this.teamInfoCommand;
            }
        }

        public ObservableCollection<TeamStats> TeamStats
        {
            get { return this.teamStats; }
            set
            {
                this.teamStats = value;
                OnPropertyChanged();
            }
        }

        public Team SelectedTeam
        {
            get { return this.selectedTeam; }
            set
            {
                this.selectedTeam = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<Team> Teams
        {
            get { return this.teams; }
            set
            {
                this.teams = value;
                this.OnPropertyChanged();
            }
        }

        public string TeamInfoSearchText
        {
            get { return this.teamInfoSearchText; }
            set
            {
                this.teamInfoSearchText = value;
                this.OnPropertyChanged();
                this.LoadTeamInfoData();
            }
        }

        public string TeamViewSearchText
        {
            get { return this.teamViewSearchText; }
            set
            {
                this.teamViewSearchText = value;
                this.OnPropertyChanged();
                this.LoadTeamViewData();
            }
        }

        #endregion

        #region Methods

        private void DeleteTeam(object obj)
        {
            this.teams.Remove(this.selectedTeam);

        }

        private void TeamInfo(object obj)
        {
            TabablzControl teamViewTabablzControl = (TabablzControl) obj;
            teamViewTabablzControl.SelectedIndex = 1;
        }

        private void OpenTeamAddView(object obj)
        {
            var teamAddView = new TeamAddView();
            teamAddView.ShowDialog();
        }

        private void OnTeamObjReceived(Team team)
        {
            this.teams.Add(team);
        }

        private void LoadTeamInfoData()
        {
            this.TeamStats = this.teamService.TeamStatsSearch(this.teamInfoSearchText).ToObservableCollection();
        }

        private void LoadTeamViewData()
        {
            this.Teams =
                this.teamService.GetAllTeams()
                    .Where(x => x.Name.Value.ToLower().Contains(this.teamViewSearchText.ToLower()))
                    .ToObservableCollection();
        }

        public void LoadData()
        {
            this.Teams = this.teamService.GetAllTeams().ToObservableCollection();
            this.teamStats = this.teamService.TeamStatsSearch(this.teamInfoSearchText).ToObservableCollection();
        }
    }
}

    #endregion
    
