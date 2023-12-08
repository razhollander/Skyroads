using CoreDomain.GameDomain.GameStateDomain.LobbyDomain.Modules.LobbyUi;
using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services.GameStates;

namespace CoreDomain.GameDomain.GameStateDomain.LobbyDomain
{
    public class EnterLobbyGameStateCommand : CommandSyncOneParameter<LobbyGameStateEnterData, EnterLobbyGameStateCommand>
    {
        private readonly ILobbyUiModule _lobbyUiModule;
        private readonly LobbyGameStateEnterData _lobbyGameStateEnterData;
        private ILevelsService _levelsService;

        public EnterLobbyGameStateCommand(LobbyGameStateEnterData lobbyGameStateEnterData, ILobbyUiModule lobbyUiModule, ILevelsService levelsService)
        {
            _lobbyGameStateEnterData = lobbyGameStateEnterData;
            _lobbyUiModule = lobbyUiModule;
            _levelsService = levelsService;
        }

        public override void Execute()
        {
            var levelsAmount = _levelsService.GetLevelsAmount();
            _lobbyUiModule.CreateLobbyUi(levelsAmount);
        }
    }
}