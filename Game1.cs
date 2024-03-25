using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Model;

namespace Pong;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Bar _bar1;
    private Bar _bar2;
    private Texture2D _barTexture;
    private Texture2D _background;
    private float _bar2PositionX;
    private float _bar2PositionY;
    private float _bar1PositionY;

    private Ball _ball;
    private Texture2D _ballTexture;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();

       _bar2PositionX = _graphics.PreferredBackBufferWidth - _barTexture.Width;
        _bar2PositionY = (_graphics.PreferredBackBufferHeight - _barTexture.Height) / 2.0f;

        _bar1PositionY = (_graphics.PreferredBackBufferHeight - _barTexture.Height) / 2.0f;

        _bar1 = new Bar(this, new Vector2(0.0f, _bar1PositionY), _barTexture, Keys.W, Keys.S);

        _bar2 = new Bar(this, new Vector2(_bar2PositionX, _bar2PositionY), _barTexture, Keys.Up, Keys.Down);

        _ball = new Ball(this, _ballTexture);
        _ball.SetStartPosition();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _background = Content.Load<Texture2D>("assets/background");
        _barTexture = Content.Load<Texture2D>("assets/bar");
        _ballTexture = Content.Load<Texture2D>("assets/ball");

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        // TODO: Add your update logic here
        _bar1.Update();
        _bar2.Update();
        _ball.Update();

        _bar1.HasCollide(_ball);
        _bar2.HasCollide(_ball);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _bar1.Draw(_spriteBatch);
        _bar2.Draw(_spriteBatch);
        _ball.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
