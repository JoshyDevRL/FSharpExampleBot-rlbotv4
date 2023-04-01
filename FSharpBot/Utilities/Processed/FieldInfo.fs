namespace Bot.Utilities.Processed

open System.Numerics

type GoalInfo(goalInfo: rlbot.flat.GoalInfo) =
    let direction = new Vector3(goalInfo.Direction.Value.X, goalInfo.Direction.Value.Y, goalInfo.Direction.Value.Z)
    let location = new Vector3(goalInfo.Location.Value.X, goalInfo.Location.Value.Y, goalInfo.Location.Value.Z)
    let teamNum = goalInfo.TeamNum

    member this.Direction = direction
    member this.Location = location
    member this.TeamNum = teamNum


type BoostPad(boostPad: rlbot.flat.BoostPad) =
    let location = new Vector3(boostPad.Location.Value.X, boostPad.Location.Value.Y, boostPad.Location.Value.Z)
    let isFullBoost = boostPad.IsFullBoost

    member this.Location = location
    member this.IsFullBoost = isFullBoost


type FieldInfo(fieldInfo: rlbot.flat.FieldInfo) =
    let goals =
        Array.init fieldInfo.GoalsLength (fun i -> 
            new GoalInfo(fieldInfo.Goals(i).Value))
    let boostPads = 
        Array.init fieldInfo.BoostPadsLength (fun i ->
            new BoostPad(fieldInfo.BoostPads(i).Value))
    
    member this.Goals = goals
    member this.BoostPads = boostPads
