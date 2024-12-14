using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CarDealers.Models;
using CarDealers.Views;
using DynamicData;
using DynamicData.Binding;
using MsBox.Avalonia;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CarDealers.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    /*private readonly SourceList<Sale> _salesSource = new();*/
    private List<Sale>? _sales;

    [Reactive] public string FilePath { get; set; }

    public ObservableCollection<int> Years { get; } = [];

    /*private int? _selectedYear;
    public int? SelectedYear
    {
        get => _selectedYear;
        set => this.RaiseAndSetIfChanged(ref _selectedYear, value);
    }*/
    [Reactive] public int? SelectedYear { get; set; }

    /*private readonly ReadOnlyObservableCollection<Sale> _salesReadOnly;
    public ReadOnlyObservableCollection<Sale> Sales => _salesReadOnly;*/
    public ObservableCollection<Sale> Sales { get; } = [];

    public ReactiveCommand<Unit, Unit> OpenFileCommand { get; }

    public MainWindowViewModel()
    {
        OpenFileCommand = ReactiveCommand.Create(OpenJsonFile);

        /*_salesSource
            .Connect()
            .Filter(sale => sale.Year == SelectedYear)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _salesReadOnly)
            .Subscribe();*/

        this
            .WhenValueChanged(x => x.SelectedYear)
            .WhereNotNull()
            .Subscribe(year =>
            {
                Sales.Clear();
                Sales.AddRange(_sales!.Where(sale => sale.Year == year));
            });
    }

    private async void OpenJsonFile()
    {
        var topLevel = TopLevel.GetTopLevel(new MainWindow());
        var files = await topLevel!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Json File",
            AllowMultiple = false,
            FileTypeFilter =
            [
                new FilePickerFileType("All json files")
                {
                    Patterns =
                    [
                        "*.json"
                    ],
                    MimeTypes = null,
                    AppleUniformTypeIdentifiers = null,
                }
            ],
            SuggestedStartLocation = await topLevel.StorageProvider.TryGetFolderFromPathAsync("C:\\")
        });
        if (files.Count >= 1)
        {
            FilePath = files[0].Path.AbsolutePath;

            await using var stream = await files[0].OpenReadAsync();
            _sales = await JsonSerializer.DeserializeAsync<List<Sale>>(stream);

            if (_sales is null)
            {
                await MessageBoxManager
                    .GetMessageBoxStandard("ОШИБКА", "Список не сформирован")
                    .ShowAsync();
            }
            else
            {
                var years = _sales
                    .Select(sale => sale.Year)
                    .Distinct()
                    .OrderBy(year => year);

                /*_salesSource.Clear();
                _salesSource.AddRange(sales);*/

                Years.Clear();
                Years.AddRange(years);
            }
        }
    }
}
