using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong.Model;

public class Ball
{
    public const float SPEED = 2.5F;
    private Texture2D _texture;
    private Vector2 _position;
    private Vector2 _ballDirection;
    private Game _game;
    bool outOfLimites = false;
    Random _random;

    public Vector2 BallDirection { get => _ballDirection; }

    public Ball(Game game, Texture2D texture)
    {
        _game = game;
        _texture = texture;
    }

    public void SetStartPosition()
    {
        var viewport = _game.GraphicsDevice.Viewport;
        _random = new Random();

        _position.Y = (viewport.Height / 2) - (_texture.Height / 2);
        _position.X = (viewport.Width / 2) - (_texture.Width / 2);

        /*_ballDirection = new Vector2(GetRandom());
        _ballDirection.Normalize();*/

        _ballDirection = new Vector2(SPEED);
        outOfLimites = false;
    }

    private float GetRandom()
    {
        return (_random.NextSingle() * 2.0f) - 1.0f;
    }

    // Verificando a coalisão da bola com as barras
    public Rectangle GetBounds()
    {
        return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
    }

    public void SetPosition(float x) { _position.X = x; }

    public void InvertBallDirection()
    {
        _ballDirection.X += _ballDirection.X * 0.3F;
        _ballDirection.X *= -1;
    }
    public void Update()
    {
        var viewport = _game.GraphicsDevice.Viewport;
        //Movimentação da bola
        //_position += _ballDirection + (_ballDirection * SPEED);
        _position += _ballDirection;

        // Coalisão da bola com os limites verticais
        if (_position.Y < 0)
        {
            _position.Y = 0.0f;
            _ballDirection.Y *= -1;
        }
        if (_position.Y + _texture.Height > viewport.Height)
        {
            _position.Y = viewport.Height - _texture.Height;
            _ballDirection.Y = -1;
        }

        // Coalisão da bola com as barras
        if( _position.X + _texture.Width < 0 || _position.X > viewport.Width )
        {
            outOfLimites = true;
            _ballDirection = Vector2.Zero;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }
}
