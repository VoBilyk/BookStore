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

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPage"/> class.
        /// </summary>
        /// <param name="loggerFactory">Logger factory</param>
        /// <param name="menuVisualizer">Menu visualizer</param>
        /// <param name="outputEnvironment">Output environment implementation</param>
        /// <param name="fileService">File service implementation</param>
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

        /// <summary>
        /// Saving data to storage
        /// </summary>
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

        /// <summary>
        /// Restoring data from storage
        /// </summary>
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
