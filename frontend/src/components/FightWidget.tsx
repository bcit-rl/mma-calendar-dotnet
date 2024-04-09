import { Box, Tab, Tabs } from "@mui/material";
import React from "react";
import { a11yProps } from "../utils/helpers";
import FightWidgetTabPanel from "./FightWidgetTabPanel";

interface FightWidgetProps {
  carouselArray?: Array<React.ReactNode>;
  eventNames?: Array<string>;
}

const FightWidget = (props: FightWidgetProps) => {
  const [value, setValue] = React.useState(0);

  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };
  const tabList: Array<React.ReactNode> = [];
  const tabPanels: Array<React.ReactNode> = [];

  if (props.carouselArray && props.eventNames) {
    for (let i = 0; i < props.carouselArray.length; i++) {
      tabList.push(<Tab label={props.eventNames[i]} {...a11yProps(i)} />);
      tabPanels.push(
        <FightWidgetTabPanel value={value} index={i}>
          {props.carouselArray[i]}
        </FightWidgetTabPanel>
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

export default FightWidget;
