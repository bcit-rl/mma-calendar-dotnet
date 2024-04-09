import { ReactNode } from "react";
import Fight from "../components/Fight";
import FightCarousel from "../components/FightCarousel";
import FighterAvatar from "../components/FighterAvatar";
import FighterInfo from "../components/FighterInfo";
const URL = "http://localhost:5217/api/events";
const FIGHT_URL = "http://localhost:5217/api/Fight";

export function a11yProps(index: number) {
  return {
    id: `simple-tab-${index}`,
    "aria-controls": `simple-tabpanel-${index}`,
  };
}

export function createWidget() {}

export function createFighterInfo(fighterData, imageSide) {
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

export function createFight(fightData) {
  const leftFighter = createFighterInfo(fightData.fighterA, "L");
  const rightFighter = createFighterInfo(fightData.fighterB, "R");

  return (
    <Fight
      date={`${fightData.date}`}
      weightClass={`Weight`}
      leftFighter={leftFighter}
      rightFighter={rightFighter}
      description={`${fightData.method} ${
        fightData.methodDescription ? "- " + fightData.methodDescription : ""
      } :  R${fightData.round} - ${fightData.displayClock} `}
    ></Fight>
  );
}

export async function createFightCarousel(EventData) {
  const fightList: ReactNode[] = [];
  const mainCard: ReactNode[] = [];
  const prelims: ReactNode[] = [];
  const earlyPrelims: ReactNode[] = [];

  for (const fightData of EventData.fights) {
    const fightDataResponse = await fetch(FIGHT_URL + `/${fightData.fightId}`);
    const fightJSONData = await fightDataResponse.json();
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

  return <FightCarousel fightArray={fightList}></FightCarousel>;
}

export function createCarousel() {
  const fight_list = [];
  fight_list.push(create_fight_list());
  fight_list.push(create_fight_list());
  fight_list.push(create_fight_list());
  return <FightCarousel fightArray={fight_list}></FightCarousel>;
}

export function create_fight_list() {
  const fight_list = [];

  for (let i = 0; i < 5; i++) {
    fight_list.push(
      <Fight
        date="March 16th"
        weightClass="Heavyweight"
        leftFighter={
          <FighterInfo
            image={
              <FighterAvatar
                src="https://ssl.gstatic.com/onebox/media/sports/photos/ufc/3015_yQaLiQ_96x96.png"
                Winner={false}
              />
            }
            imageSide={"L"}
            fighterName={"Tai Tuivasa"}
            fighterRecord={"15 - 6 - 0 - 0"}
          />
        }
        rightFighter={
          <FighterInfo
            image={
              <FighterAvatar
                src="https://ssl.gstatic.com/onebox/media/sports/photos/ufc/2753_6Z4w4Q_96x96.png"
                Winner={true}
              />
            }
            imageSide={"R"}
            fighterName={"Marcin Tybura"}
            fighterRecord={"24 - 8 - 0 - 0"}
          />
        }
        description={"Tai Tuivasa wins via KO/TKO"}
      ></Fight>
    );
  }
  return fight_list;
}
