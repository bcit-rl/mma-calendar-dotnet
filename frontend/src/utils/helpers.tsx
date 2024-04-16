import { ReactNode } from "react";
import Fight from "../components/Fight";
import FighterAvatar from "../components/FighterAvatar";
import FighterInfo from "../components/FighterInfo";
import { IEventData, IFight, IFighter } from "./Interfaces";
const FIGHT_URL = "http://localhost:5217/api/Fight";

export function a11yProps(index: number) {
  return {
    id: `simple-tab-${index}`,
    "aria-controls": `simple-tabpanel-${index}`,
  };
}

export function createFighterInfo(fighterData: IFighter, imageSide: string) {
  return (
    <FighterInfo
      image={
        <FighterAvatar
          src={`${fighterData.headshot}`}
          Winner={fighterData.winner ? fighterData.winner : false}
        />
      }
      imageSide={imageSide}
      fighterName={`${fighterData.firstName} ${
        fighterData.nickName ? '"' + fighterData.nickName + '"' : ""
      } ${fighterData.lastName}`}
      fighterRecord={`${fighterData.wins} - ${fighterData.losses} - ${fighterData.draws} - ${fighterData.noContests}`}
    />
  );
}

export function createDateString(date: Date) {
  const daysOfWeek = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
  const monthAbbreviations = [
    "Jan",
    "Feb",
    "Mar",
    "Apr",
    "May",
    "Jun",
    "Jul",
    "Aug",
    "Sep",
    "Oct",
    "Nov",
    "Dec",
  ];
  const hours = date.getHours() % 12 == 0 ? 12 : date.getHours() % 12;
  const minutes =
  date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
  const timeString =
    date.getHours() >= 12
      ? `${hours}:${minutes} p.M.`
      : `${hours}:${minutes} a.M.`;


  return `${daysOfWeek[date.getDay()]} ${
    monthAbbreviations[date.getMonth()]
  } ${date.getDate()}, ${timeString}`;
}

/**
 * Given fightData creates a Fight Component
 *
 * @param fightData
 * @returns Fight component with data filled out
 */
export function createFight(fightData: IFight) {
  const leftFighter: ReactNode = createFighterInfo(fightData.fighterA, "L");
  const rightFighter: ReactNode = createFighterInfo(fightData.fighterB, "R");
  const componentKey: string = `${fightData.fighterA.fighterId}-${fightData.fighterB.fighterId}`;

  return (
    <Fight
      key={componentKey}
      date={`${fightData.date}Z`}
      weightClass={`${fightData.weightClass}`}
      leftFighter={leftFighter}
      rightFighter={rightFighter}
      description={
        fightData.method
          ? `${fightData.method} ${
              fightData.methodDescription
                ? "- " + fightData.methodDescription
                : ""
            } :  R${fightData.round} - ${fightData.displayClock} `
          : ""
      }
    ></Fight>
  );
}

/**
 * Creates the list of fights to be put in an event. The data is obtained through a GET request
 * from the requested URL. The list of fights is a 2d array structured like so
 *
 * [[Main Card Fights], [Prelim Card Fights], [Early Prelim fights]]
 *
 * If there are no prelim/early prelim fights, they will not be added. Inside the arrays
 * are Fight components
 *
 * @param EventUrl URL to the API that has the data
 * @returns 2d array where the inner arrays are ReactNode[]
 */
export async function createFightList(EventUrl: string) {
  const EventData = await fetch(EventUrl).then((res) => res.json());
  const fightList: ReactNode[] = [];
  const mainCard: ReactNode[] = [];
  const prelims: ReactNode[] = [];
  const earlyPrelims: ReactNode[] = [];

  for (const fightData of EventData.fights) {
    const fightDataResponse = await fetch(FIGHT_URL + `/${fightData.fightId}`);
    const fightJSONData = await fightDataResponse.json();

    if (
      fightJSONData.method == null &&
      new Date(fightJSONData.date) < new Date()
    ) {
      continue;
    }

    const fight = await createFight(fightJSONData);

    if (fightJSONData.cardSegment == "Main Card") {
      mainCard.push(fight);
    }
    if (fightJSONData.cardSegment == "Prelims") {
      prelims.push(fight);
    }
    if (fightJSONData.cardSegment == "Early Prelims") {
      earlyPrelims.push(fight);
    }
  }

  if (mainCard.length > 0) {
    fightList.push(mainCard);
  }

  if (prelims.length > 0) {
    fightList.push(prelims);
  }
  if (earlyPrelims.length > 0) {
    fightList.push(earlyPrelims);
  }

  return fightList;
}

/**
 * Binary search algo for finding the date in the array that is closest to today's date
 * @param nums
 * @param target
 * @returns Number that is the index in nums where the closest date is.
 */
export function binarySearch(nums: IEventData[], target: Date): number {
  let left: number = 0;
  let right: number = nums.length - 1;
  let mid: number = 0;
  target.setHours(0, 0, 0, 0);
  while (left <= right) {
    mid = Math.floor((left + right) / 2);
    const midDate = new Date(nums[mid].eventDate);
    midDate.setHours(0, 0, 0, 0);
    if (midDate === target) return mid;
    if (target < midDate) right = mid - 1;
    else left = mid + 1;
  }

  return mid;
}
