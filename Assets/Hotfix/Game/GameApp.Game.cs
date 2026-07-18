#if ENABLE_GAME_FRAME_X_GAME
using Godot.Hotfix.Game.Component;
using GameFrameX.Runtime;

public static partial class GameApp
{
    public static GameConstantComponent GameConstant
    {
        get
        {
            if (_gameConstant == null)
            {
                _gameConstant = GameEntry.GetComponent<GameConstantComponent>();
            }
            return _gameConstant;
        }
    }

    private static GameConstantComponent _gameConstant;

    public static SaveComponent Save
    {
        get
        {
            if (_save == null)
            {
                _save = GameEntry.GetComponent<SaveComponent>();
            }
            return _save;
        }
    }

    private static SaveComponent _save;

    public static FightComponent Fight
    {
        get
        {
            if (_fight == null)
            {
                _fight = GameEntry.GetComponent<FightComponent>();
            }
            return _fight;
        }
    }

    private static FightComponent _fight;

    public static GameLoopComponent GameLoop
    {
        get
        {
            if (_gameLoop == null)
            {
                _gameLoop = GameEntry.GetComponent<GameLoopComponent>();
            }
            return _gameLoop;
        }
    }

    private static GameLoopComponent _gameLoop;
}
#endif