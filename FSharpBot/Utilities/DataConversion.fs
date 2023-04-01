namespace Bot.Utilities

module DataConversion =
    open System.Numerics

    let toVector3 (vector: rlbot.flat.Vector3) =
        Vector3(vector.X, vector.Y, vector.Z)

    let toVector2 (vector: rlbot.flat.Vector3) =
        Vector2(vector.X, vector.Y)