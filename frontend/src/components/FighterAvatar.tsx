import { Avatar } from "@mui/material";

interface FighterAvatarProps {
  src: string;
  Winner: boolean;
}

const FighterAvatar = (props: FighterAvatarProps) => {
  return (
    <Avatar
      src={props.src}
      sx={{
        width: 55,
        height: 55,
        border: 1,
        margin: "0px 15px 0px",
        borderColor: props.Winner ? "Grey" : "Green",
      }}
    />
  );
};

export default FighterAvatar;
