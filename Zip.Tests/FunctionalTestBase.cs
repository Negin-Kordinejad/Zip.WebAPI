using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Zip.WebAPI.MappingProfiles;
using Zip.WebAPI.Repository;
using Zip.WebAPI.Services;


namespace Zip.Tests
{
    public class FunctionalTestBase
    {
        private readonly Mock<ILogger<UserService>> _loggerUser = new Mock<ILogger<UserService>>();
        protected readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();

        private readonly Mock<ILogger<AcountService>> _loggerAcount = new Mock<ILogger<AcountService>>();
        protected readonly Mock<IAcountRepository> _acountRepository = new Mock<IAcountRepository>();
        protected readonly Mock<ICreditValidator> _creditValidator = new Mock<ICreditValidator>();

        protected UserService UserService;
        protected AcountService AcountService;
        protected IMapper Mapper;


        [TestInitialize]
        public void Initialize()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserDtoMapper());
                mc.AddProfile(new AcountDtoMapper());
            });
            Mapper = mapperConfig.CreateMapper();
            UserService = new UserService(_loggerUser.Object, Mapper, _userRepository.Object);
            AcountService = new AcountService(_loggerAcount.Object, Mapper, _acountRepository.Object, _creditValidator.Object);

        }
    }
}