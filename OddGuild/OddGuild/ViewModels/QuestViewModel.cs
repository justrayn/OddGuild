using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OddGuild.Models;
using System.Linq;
using System.Collections.Generic;

namespace OddGuild.ViewModels; 

public partial class QuestViewModel : ObservableObject
{
    // The master list of all quests
    private readonly List<Quest> _allQuests = new();

    // The list actually bound to the UI CollectionView
    [ObservableProperty]
    private ObservableCollection<Quest> _availableQuests = new();

    // --- VIEW QUEST DETAILS PROPERTIES ---
    [ObservableProperty]
    private Quest? _selectedQuest;

    [ObservableProperty]
    private bool _isDetailsVisible;

    // --- SEARCH PROPERTY ---
    [ObservableProperty]
    private string? _searchQuery;

    // --- ADD QUEST FORM PROPERTIES ---
    [ObservableProperty]
    private bool _isFormVisible;

    [ObservableProperty]
    private string? _newQuestTitle;

    [ObservableProperty]
    private string? _newQuestDescription;

    [ObservableProperty]
    private int _newQuestBounty;

    [ObservableProperty]
    private string? _newQuestLocation;

    // --- TAB PROPERTIES ---
    [ObservableProperty]
    private bool _isAllTabActive = true;

    [ObservableProperty]
    private bool _isGeneralTabActive;

    [ObservableProperty]
    private bool _isMyTabActive;

    public QuestViewModel()
    {
        LoadDummyData();
        FilterQuests();
    }

    // --- COMMANDS: VIEW QUEST DETAILS ---
    [RelayCommand]
    private void SelectQuest(Quest quest)
    {
        SelectedQuest = quest;
        IsDetailsVisible = true;
    }

    [RelayCommand]
    private void CloseDetails()
    {
        IsDetailsVisible = false;
        SelectedQuest = null;
    }

    // --- COMMANDS: ADD QUEST FORM ---
    [RelayCommand]
    private void ToggleForm()
    {
        IsFormVisible = !IsFormVisible;
    }

    [RelayCommand]
    private void PostQuest()
    {
        if (string.IsNullOrWhiteSpace(NewQuestTitle) || string.IsNullOrWhiteSpace(NewQuestDescription))
            return;

        var newQuest = new Quest
        {
            Id = _allQuests.Any() ? _allQuests.Max(q => q.Id) + 1 : 1,
            Title = NewQuestTitle,
            Description = NewQuestDescription,
            Bounty = NewQuestBounty,
            Location = NewQuestLocation,
            Type = "CUSTOM", 
            IsMine = true
        };

        _allQuests.Add(newQuest);

        // Reset fields
        NewQuestTitle = string.Empty;
        NewQuestDescription = string.Empty;
        NewQuestBounty = 0;
        NewQuestLocation = string.Empty;
        
        // Hide form and switch to My Quests tab
        IsFormVisible = false;
        ShowMyQuests();
    }

    // --- COMMANDS: TABS & FILTERING ---
    [RelayCommand]
    private void ShowAllQuests()
    {
        SetTabState(all: true, general: false, my: false);
        FilterQuests();
    }

    [RelayCommand]
    private void ShowGeneralQuests()
    {
        SetTabState(all: false, general: true, my: false);
        FilterQuests();
    }

    [RelayCommand]
    private void ShowMyQuests()
    {
        SetTabState(all: false, general: false, my: true);
        FilterQuests();
    }

    private void SetTabState(bool all, bool general, bool my)
    {
        IsAllTabActive = all;
        IsGeneralTabActive = general;
        IsMyTabActive = my;
    }

    private void FilterQuests()
    {
        IEnumerable<Quest> filteredList;

        if (IsGeneralTabActive)
            filteredList = _allQuests.Where(q => !q.IsMine);
        else if (IsMyTabActive)
            filteredList = _allQuests.Where(q => q.IsMine);
        else
            filteredList = _allQuests;

        if (!string.IsNullOrWhiteSpace(SearchQuery))
            filteredList = filteredList.Where(q => q.Title?.Contains(SearchQuery, System.StringComparison.OrdinalIgnoreCase) == true);

        AvailableQuests = new ObservableCollection<Quest>(filteredList);
    }

    private void LoadDummyData()
    {
        _allQuests.Add(new Quest { Id = 1, Title = "Clear the Rat Cellar", Type = "COMBAT", Description = "The tavern keeper needs help clearing giant rats.", Bounty = 50, Location = "Local Tavern", IsMine = false });
        _allQuests.Add(new Quest { Id = 2, Title = "Deliver the Iron Ingot", Type = "FETCH", Description = "Take this heavy iron to the blacksmith.", Bounty = 120, Location = "Blacksmith Forge", IsMine = false });
        _allQuests.Add(new Quest { Id = 3, Title = "Investigate Whispering Grove", Type = "EXPLORE", Description = "Strange sounds are coming from the forest.", Bounty = 500, Location = "Dark Forest", IsMine = true });
    }
}