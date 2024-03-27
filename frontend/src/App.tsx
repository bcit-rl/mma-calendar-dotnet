import "./App.css";
import FighterInfo from "./components/FighterInfo";
import Fight from "./components/Fight";
import FightCarousel from "./components/FightCarousel";
import FighterAvatar from "./components/FighterAvatar";

function create_fight_list() {
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

function App() {
  const fight_list = [];
  fight_list.push(create_fight_list());
  fight_list.push(create_fight_list());
  fight_list.push(create_fight_list());
  return (
    <div className="carousel-container">
      <FightCarousel fightArray={fight_list}></FightCarousel>
    </div>
  );
}

export default App;
