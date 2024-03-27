import * as React from "react";
import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";
import Box from "@mui/material/Box";
import FightTabPanel from "./FightTabPanel";

function a11yProps(index: number) {
  return {
    id: `simple-tab-${index}`,
    "aria-controls": `simple-tabpanel-${index}`,
  };
}

interface FightCarouselProps {
  fightArray?: Array<React.ReactNode>;
}

const FightCarousel = (props: FightCarouselProps) => {
  const [value, setValue] = React.useState(0);

  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };
  const tabList: Array<React.ReactNode> = [];
  const tabPanels: Array<React.ReactNode> = [];
  const cardSegments: readonly [string, string, string] = [
    "Main Card",
    "Prelims",
    "Early Prelims",
  ];

  if (props.fightArray) {
    for (let i = 0; i < props.fightArray.length; i++) {
      tabList.push(<Tab label={cardSegments[i]} {...a11yProps(i)} />);
      tabPanels.push(
        <FightTabPanel value={value} index={i}>
          {props.fightArray[i]}
        </FightTabPanel>
      );
    }
  }

  return (
    <Box sx={{ width: "50%" }}>
      <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
        <Tabs
          value={value}
          onChange={handleChange}
          variant="scrollable"
          aria-label="basic tabs example"
        >
          {tabList}
        </Tabs>
      </Box>
      {tabPanels}
    </Box>
  );
};

export default FightCarousel;
