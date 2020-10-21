public enum GameType
{
	local,
	network
}

static public class GameConfigs
{
	static public GameType gameType;
	static public float courtWidthInUnits = 6f;
	static public float paddleOffsetFromCenterInUnits = 3.6f;
	static public float ballMinSpeed = 3f;
	static public float ballMaxSpeed = 6f;
	static public float BallMinSize = 0.3f;
	static public float BallMaxSize = 1.5f;
}
