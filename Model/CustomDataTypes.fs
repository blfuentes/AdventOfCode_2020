module CustomDataTypes

// Day 02
type PasswordPolicy = {min: int; max: int; element: string; code: string }
type PasswordPolicyOption = PasswordPolicy option

// Day 04
type HeightType = {height: int; unittype: string }
type HeightTypeOption = HeightType option

// Day 07
type ChristmasBag =  { Name: string; Size: int; Content: ChristmasBag list }
type ChristmasBagOption = ChristmasBag option