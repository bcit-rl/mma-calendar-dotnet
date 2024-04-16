import { Box, Tab, Tabs } from "@mui/material";
import { ReactNode, useState, SyntheticEvent } from "react";
import { IEventData } from "../utils/Interfaces";
import { a11yProps, binarySearch } from "../utils/helpers";
import FightWidgetTabPanel from "./FightWidgetTabPanel";

interface FightWidgetProps {
  carouselArray?: Array<ReactNode>;
  eventData: Array<IEventData>;
}

const FightWidget = (props: FightWidgetProps) => {
  const [value, setValue] = useState(binarySearch(props.eventData, new Date()));
  const handleChange = (event: SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };
  const tabList: Array<ReactNode> = [];
  const tabPanels: Array<ReactNode> = [];

  if (props.carouselArray && props.eventData) {
    for (let i = 0; i < props.carouselArray.length; i++) {
      tabList.push(
        <Tab
          key={`FightWidgetTab-${i}`}
          label={props.eventData[i].eventName}
          {...a11yProps(i)}
        />
      );
      tabPanels.push(
        <FightWidgetTabPanel
          key={`FightWidgetTabPanel-${i}`}
          value={value}
          index={i}
        >
          {props.carouselArray[i]}
        </FightWidgetTabPanel>
      );
    }
  }

  return (
    <Box sx={{ minWidth: 500, maxWidth: 800 }}>
      <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
        <Tabs
          value={value}
          onChange={handleChange}
          variant="scrollable"
          scrollButtons={true}
          aria-label="basic tabs example"
        >
          {tabList}
        </Tabs>
      </Box>
      {tabPanels}
    </Box>
  );
};

export default FightWidget;
