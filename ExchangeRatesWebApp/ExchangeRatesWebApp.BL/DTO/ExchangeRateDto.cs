namespace ExchangeRatesWebApp.BL.DTO
{
    public class ExchangeRateDto
    {
        /// <summary>
        ///     Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Наименование валюты
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        ///     Значение валюты
        /// </summary>
        public decimal CurrencyValue { get; set; }

        /// <summary>
        ///     кол-во валюты
        /// </summary>
        public int CurrencyCount { get; set; }

        /// <summary>
        ///     Дата в формате dd/mm/yyyy
        /// </summary>
        public string Date { get; set; }
    }
}