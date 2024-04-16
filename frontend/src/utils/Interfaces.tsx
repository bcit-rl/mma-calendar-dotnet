export interface IFight {
    fightId: number
    description: string
    date: string
    weightClass: string
    displayClock: string
    round: number
    method: string
    methodDescription: string
    cardSegment: string
    winner: string
    eventId: number
    matchNumber: number
    fighterA: IFighter
    fighterB: IFighter
  }
  
  export interface IFighter {
    fighterId: number
    firstName: string
    lastName: string
    nickName: string
    weight: number
    height: number
    age: number
    winner: boolean
    gender: string
    citizenship: string
    headshot: string
    wins: number
    losses: number
    draws: number
    noContests: number
    leftStance: string
    rightStance: string
  }
  
  export interface IEventData {
    eventId: number
    eventName: string
    shortName: string
    eventDate: string
    venueId: number
    venue: IVenue
    fightIdList: number[]
  }
  
  export interface IVenue {
    venueId: number
    name: string
    city: string
    state: string
    country: string
    indoor: boolean
  }