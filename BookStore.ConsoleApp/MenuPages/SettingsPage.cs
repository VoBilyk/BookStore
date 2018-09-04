namespace BookStore.ConsoleApp.MenuPages
{
    using System;

    using BookStore.BLL.Interfaces;
    using BookStore.ConsoleApp.Interfaces;
    using BookStore.Shared.Interfaces;
    using BookStore.Shared.Resources;

    /// <summary>
    /// Page from which started design
    /// </summary>
    public class SettingsPage : ISettingsPage
    {
        private readonly ICustomLogger _logger;
        private readonly IMenuVisualizer _menuVisualizer;
        private readonly IOutputEnvironment _outputEnvironment;
        private readonly IFileService _fileService;

        public SettingsPage(
            ICustomLoggerFactory loggerFactory,
            IMenuVisualizer menuVisualizer,
            IOutputEnvironment outputEnvironment,
            IFileService fileService)
        {
            this._logger = loggerFactory.CreateLogger<SettingsPage>();

            this._menuVisualizer = menuVisualizer;
            this._outputEnvironment = outputEnvironment;
            this._fileService = fileService;
        }

        /// <inheritdoc/>
        public void Display()
        {
            var menu = _menuVisualizer.FactoryMethod()
                .Add(Resource.Save, () => SaveDb())
                .Add(Resource.Restore, () => RestoreDb());
            menu.Display();
        }

        public void SaveDb()
        {
            _outputEnvironment.Write($"{Resource.EnterFileName}: ");
            var fileName = _outputEnvironment.Read();

            try
            {
                _fileService.SaveToFile(fileName);

                _outputEnvironment.WriteLine(Resource.StorageSaved);
                _logger.Info(Resource.StorageSaved);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        public void RestoreDb()
        {
            _outputEnvironment.Write($"{Resource.EnterFileName}: ");
            var fileName = _outputEnvironment.Read();

            try
            {
                _fileService.ReadFromFile(fileName);

                _outputEnvironment.WriteLine(Resource.StoregeRestored);
                _logger.Info(Resource.StoregeRestored);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }
    }
}
