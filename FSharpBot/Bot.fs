namespace FSHARPBOT
open System.Drawing
open System.Numerics
open Bot.Utilities.Processed
open RLBotDotNet

// Define the Bot class as a type
type Bot(botName: string, botTeam: int, botIndex: int) =
    inherit RLBotDotNet.Bot(botName, botTeam, botIndex)

    // Implement the GetOutput method
    override this.GetOutput(gameTickPacket: rlbot.flat.GameTickPacket) =
        // Process the gameTickPacket and convert it to our own internal data structure.
        let packet = new Packet(gameTickPacket)

        // Get the data required to drive to the ball.
        let ballLocation = packet.Ball.Physics.Location
        let carLocation = packet.Players.[botIndex].Physics.Location
        let carRotation = packet.Players.[botIndex].Physics.Rotation

        // Find where the ball is relative to us.
        let ballRelativeLocation = Orientation.RelativeLocation(carLocation, ballLocation, carRotation)

        // Decide which way to steer in order to get to the ball.
        // If the ball is to our left, we steer left. Otherwise we steer right.
        let steer =
            if ballRelativeLocation.Y > 0f then 1.0f else -1.0f
            
        let direction = if steer > 0.0f then "Right" else "Left"

        // This controller will contain all the inputs that we want the bot to perform.
        new Controller(Throttle=1f, Steer=steer)

    // use processed versions of those objects instead.
    member this.GetFieldInfo() = new FieldInfo(base.GetFieldInfo()) 
    member this.GetBallPrediction() = new BallPrediction(base.GetBallPrediction()) 