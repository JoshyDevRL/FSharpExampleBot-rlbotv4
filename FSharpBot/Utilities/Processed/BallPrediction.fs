namespace Bot.Utilities.Processed

open System.Numerics

type PredictionSlice(predictionSlice : rlbot.flat.PredictionSlice) =
    let physics = new Physics(predictionSlice.Physics.Value)
    let gameSeconds = predictionSlice.GameSeconds

    member this.Physics = physics
    member this.GameSeconds = gameSeconds

type BallPrediction(ballPrediction : rlbot.flat.BallPrediction) =
    let slices = Array.init ballPrediction.SlicesLength (fun i -> 
        new PredictionSlice(ballPrediction.Slices(i).Value))
    let length = ballPrediction.SlicesLength

    member this.Slices = slices
    member this.Length = length
