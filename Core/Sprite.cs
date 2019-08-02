using DotNetNinja.Core.Map;
using DotNetNinja.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace DotNetNinja.Core
{
    internal delegate void EntityTypeEventHandler(EntityType entityType, Point location);

    internal class Sprite : GameEntity, ISprite
    {
        private readonly Color defaultColorKey = Color.FromArgb(75, 75, 75);
        private static readonly PointF defaultAcceleration = new PointF(GameState.ENTITIES_MOVE_SPEED, GameState.ENTITIES_MOVE_SPEED);

        public static int CalculateNextFrame(double gameTime, int framesCount)
        {
            return (int)((gameTime * GameState.FRAME_RATE) % (double)framesCount);
        }

        private ImageAttributes attributes;

        private List<Rectangle> frameRectangles;

        private List<Bitmap> frames;

        private bool animationEnded;

        private Color colorKey;

        public Sprite(float x, float y, Entity entity, bool flip = false)
            : base(entity)
        {
            this.IsAnimationEnabled = true;
            this.CreateFrames(x, y, entity, flip);
        }

        public event EntityTypeEventHandler UpdateSprite;

        public PointF Acceleration { get; set; }

        public int CurrentFrameIndex { get; set; }

        public bool Flip { get; set; }

        public PointF Location { get; set; }

        public Point Position { get; set; }

        public bool IsAnimationEnabled { get; set; }

        public bool IsStateChangable
        {
            get
            {
                return this.Entity.Category == EntityCategoryType.Door;
            }
        }

        public int FramesCount
        {
            get
            {
                return this.frames.Count;
            }
        }

        public SizeF Size { get; set; }

        public PointF Velocity { get; set; }

        protected void OnUpdateTile(EntityType type)
        {
            if (this.UpdateSprite != null)
            {
                this.UpdateSprite(type, this.Position);
            }
        }

        public override void Update(double gameTime, double elapsedTime)
        {
            this.Location.X += this.Velocity.X * (float)elapsedTime;
            this.Location.Y += this.Velocity.Y * (float)elapsedTime;

            this.Velocity.X += Math.Sign(this.Velocity.X) * this.Acceleration.X * (float)elapsedTime;
            this.Velocity.Y += Math.Sign(this.Velocity.Y) * this.Acceleration.Y * (float)elapsedTime;
        }

        public override void Draw(IRenderer renderer)
        {
            int currentFrameIndex = this.CurrentFrameIndex;
            if (!this.IsAnimationEnabled)
            {
                currentFrameIndex = 0;
            }
            else
            {
                if (!this.animationEnded && this.IsStateChangable && currentFrameIndex == this.frames.Count - 1)
                {
                    this.animationEnded = true;
                    this.Entity.IsPassable = true;
                }

                if (this.animationEnded)
                {
                    currentFrameIndex = this.frames.Count - 1;
                }
            }

            var currentFrameRectangle = this.frameRectangles[currentFrameIndex];
            var currentFrame = this.frames[currentFrameIndex];

            if (currentFrameRectangle == Rectangle.Empty)
            {
                renderer.DrawImage(currentFrame, this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
            }
            else
            {
                Rectangle outputRect = Rectangle.Empty;
                if (this.Flip)
                {
                    outputRect = new Rectangle(
                        (int)this.Location.X + (int)this.Size.Width,
                        (int)this.Location.Y, -(int)this.Size.Width,
                        (int)this.Size.Height);
                }
                else
                {
                    outputRect = new Rectangle(
                        (int)this.Location.X,
                        (int)this.Location.Y,
                        (int)this.Size.Width,
                        (int)this.Size.Height);
                }

                renderer.DrawImage(
                    currentFrame, outputRect,
                    currentFrameRectangle.X,
                    currentFrameRectangle.Y,
                    currentFrameRectangle.Width,
                    currentFrameRectangle.Height,
                    GraphicsUnit.Pixel,
                    this.attributes);
            }
        }

        protected void CreateFrames(float x, float y, Entity entity, bool flip = false)
        {
            if (entity.Tile.IsTransparent)
            {
                this.SetColorKey(entity.ColorKey ?? this.defaultColorKey);
            }

            this.Flip = flip;
            this.frameRectangles = new List<Rectangle>();
            this.frames = new List<Bitmap>();
            this.Velocity = PointF.Empty;
            this.Acceleration = defaultAcceleration;

            if (x > Area.MapSizeX && y > Area.MapSizeY)
            {
                this.Location = new PointF(x, y);
            }
            else
            {
                this.Position = new Point((int)x, (int)y);
                this.Location = new PointF(x * Tile.TileSizeX + Area.AreaOffsetX,
                                           y * Tile.TileSizeY + Area.AreaOffsetY);
            }

            var entityRectangle = entity.Tile.Rectangle;
            var entityFramesCount = entity.Tile.FramesCount;
            var entityBitmap = entity.Tile.Bitmap;
            this.Size = new SizeF(entityRectangle.Width / entityFramesCount, entityRectangle.Height);

            for (int i = 0; i < entityFramesCount; i++)
            {
                this.frames.Add(entityBitmap);
                this.frameRectangles.Add(new Rectangle(entityRectangle.X + i * entityRectangle.Width / entityFramesCount,
                    entityRectangle.Y, entityRectangle.Width / entityFramesCount, entityRectangle.Height));
            }
        }

        private void SetColorKey(Color value)
        {
            this.colorKey = value;
            this.attributes = new ImageAttributes();
            this.attributes.SetColorKey(this.colorKey, this.colorKey);
        }
    }
}