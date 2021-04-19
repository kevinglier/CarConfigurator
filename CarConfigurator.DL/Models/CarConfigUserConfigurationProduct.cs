namespace CarConfigurator.DL.Models
{
    public class CarConfigUserConfigurationProduct
    {
        public int Id { get; }
        public int CarConfigUserConfigurationId { get; }
        public int OptionId { get; }
        public int SelectedOptionProductId { get; }

        public CarConfigUserConfigurationProduct(int id, int carConfigUserConfigurationId, int optionId,
            int selectedOptionProductId)
        {
            Id = id;
            CarConfigUserConfigurationId = carConfigUserConfigurationId;
            OptionId = optionId;
            SelectedOptionProductId = selectedOptionProductId;
        }

        public CarConfigUserConfigurationProduct(int carConfigUserConfigurationId, int optionId,
            int selectedOptionProductId)
        {
            CarConfigUserConfigurationId = carConfigUserConfigurationId;
            OptionId = optionId;
            SelectedOptionProductId = selectedOptionProductId;
        }
    }
}