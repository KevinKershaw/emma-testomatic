module etconfig
open FSharp.Configuration

type ETConfig = YamlConfig<"config.yaml">
let config = ETConfig()
config.Load ".\config.yaml"

let mutable baseEmmaUrl = config.baseEmmaUrl.ToString()
let mutable baseDataportUrl = config.baseDataportUrl.ToString()
