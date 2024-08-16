using Bede.Spin.BlazorUi.ViewModels;
using Bede.Spin.Core.Domain;
using Bede.Spin.Core.Extensions;
using Bede.Spin.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Bede.Spin.BlazorUi.Components.Pages;

public partial class Game
{
    const int columns = 3;
    const int rows = 4;
    int counter = 1;
    private List<string[]> listOfIcons;
    private SlotMatchine _slotMachine;
    private Player _player;
    private string content;

    [Parameter] public decimal InitialBalance { get; set; } = 0;
    
    private StakeViewModel model = new StakeViewModel();
    private EditContext editContext;
    private ValidationMessageStore validationMessageStore;

    protected override void OnInitialized()
    {
        editContext = new EditContext(model);
        validationMessageStore = new ValidationMessageStore(editContext);

        _player = new Player();
        _slotMachine = new SlotMatchine(new ProbabilitiesGenerator(), new PayoutCalculator(), rows, columns);
        MapSpinResultsIntoListOfIcons(RandomStartingItems());
    }
    
    private async Task Deposit()
    {
        _player.Deposit(InitialBalance);
        AddLine($"New deposit: {InitialBalance}");
        InitialBalance = 0;
    }

    private void Spin()
    {
        var stake = model.Stake;
        if (stake > _player.Balance)
        {
            validationMessageStore.Add(() => model.Stake, "You can't stake more than your balance.");
            editContext.NotifyValidationStateChanged();
            return;
        }

        var wonPayout = _slotMachine.Spin(_player, model.Stake);
        AddLine($"{counter++}, You staked: {stake}, You won: {wonPayout.ToString().PadRight(6)} => {_player.Balance}");
        
        var results = _slotMachine.SpinResultAsRowsAndColumns;
        MapSpinResultsIntoListOfIcons(results);
    }
    
    private List<List<SpinItem>> RandomStartingItems()
    {
        var probabilities = new ProbabilitiesGenerator().GenerateArray(rows*columns);
        var rowsAndColumns = probabilities.MapToSpinItems().ChopListIntoRowsAndColumns(rows, columns);
        return rowsAndColumns;
    }
    
    private void MapSpinResultsIntoListOfIcons(List<List<SpinItem>> spinResultGrid)
    {
        listOfIcons = new List<string[]>();
        foreach (var rowLine in spinResultGrid)
        {
            listOfIcons.Add(rowLine.MapSpinItemsToIcons());
        }
    }
    
    private void AddLine(string line)
    {
        content = $"{DateTime.Now:T} {line} \n" + content;
    }

    private void ResetValidation(string field)
    {
        if (editContext is not null)
        {
            validationMessageStore.Clear(new FieldIdentifier(model, field));
            editContext.NotifyValidationStateChanged();
        }
    }

}