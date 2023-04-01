namespace Bot.Utilities.Processed

open System.Numerics

type Orientation(rotator: rlbot.flat.Rotator option) =
    let pitch, roll, yaw =
        match rotator with
        | Some r -> r.Pitch, r.Roll, r.Yaw
        | None -> 0.0f, 0.0f, 0.0f

    let cp = System.Math.Cos(float pitch)
    let cy = System.Math.Cos(float yaw)
    let cr = System.Math.Cos(float roll)
    let sp = System.Math.Sin(float pitch)
    let sy = System.Math.Sin(float yaw)
    let sr = System.Math.Sin(float roll)

    let forward = Vector3(float32 cp * float32 cy, float32 cp * float32 sy, float32 sp)
    let right = Vector3(float32 cy * float32 sp * float32 sr - float32 cr * float32 sy, float32 sy * float32 sp * float32 sr + float32 cr * float32 cy, float32 -cp * float32 sr)
    let up = Vector3(float32 -cr * float32 cy * float32 sp - float32 sr * float32 sy, float32 -cr * float32 sy * float32 sp + float32 sr * float32 cy, float32 cp * float32 cr)

    member val Pitch = pitch with get
    member val Roll = roll with get
    member val Yaw = yaw with get 
    member val Forward = forward with get
    member val Right = right with get
    member val Up = up with get

    static member RelativeLocation(start: Vector3, target: Vector3, orientation: Orientation) =
        let startToTarget = target - start
        let x = Vector3.Dot(startToTarget, orientation.Forward)
        let y = Vector3.Dot(startToTarget, orientation.Right)
        let z = Vector3.Dot(startToTarget, orientation.Up)
        Vector3(x, y, z)


type Physics(physics: rlbot.flat.Physics) =
    let location = Vector3(physics.Location.Value.X, physics.Location.Value.Y, physics.Location.Value.Z)
    let velocity = Vector3(physics.Velocity.Value.X, physics.Velocity.Value.Y, physics.Velocity.Value.Z)
    let angularVelocity = Vector3(physics.AngularVelocity.Value.X, physics.AngularVelocity.Value.Y, physics.AngularVelocity.Value.Z)
    let rotation = Orientation(Some(physics.Rotation.Value))
    member this.Location with get() = location
    member this.Velocity with get() = velocity
    member this.AngularVelocity with get() = angularVelocity
    member this.Rotation with get() = rotation


type Ball(ballInfo: rlbot.flat.BallInfo) =
    member this.Physics with get() = new Physics(ballInfo.Physics.Value)


type Player(playerInfo: rlbot.flat.PlayerInfo) =
    member val Boost = playerInfo.Boost with get
    member val DoubleJumped = playerInfo.DoubleJumped with get
    member val HasWheelContact = playerInfo.HasWheelContact with get
    member val IsSupersonic = playerInfo.IsSupersonic with get
    member val Jumped = playerInfo.Jumped with get
    member val Name = playerInfo.Name with get
    member val Physics = new Physics(playerInfo.Physics.Value) with get
    member val Team = playerInfo.Team with get


type BoostPadState(boostPadState: rlbot.flat.BoostPadState) =
    member val Timer = boostPadState.Timer with get
    member val IsActive = boostPadState.IsActive with get


type GameInfo(gameInfo: rlbot.flat.GameInfo) =
    member val SecondsElapsed = gameInfo.SecondsElapsed with get
    member val GameTimeRemaining = gameInfo.GameTimeRemaining with get
    member val IsKickoffPause = gameInfo.IsKickoffPause with get


type TeamInfo(teamInfo: rlbot.flat.TeamInfo) =
    member val TeamIndex = teamInfo.TeamIndex with get
    member val Score = teamInfo.Score with get


type Packet(packet: rlbot.flat.GameTickPacket) =
    member val Players = Array.init packet.PlayersLength (fun i -> Player(packet.Players(i).Value)) with get
    member val BoostPadStates = Array.init packet.BoostPadStatesLength (fun i -> BoostPadState(packet.BoostPadStates(i).Value)) with get
    member val Ball = Ball(packet.Ball.Value) with get
    member val GameInfo = GameInfo(packet.GameInfo.Value) with get
    member val Teams = Array.init packet.TeamsLength (fun i -> TeamInfo(packet.Teams(i).Value)) with get
