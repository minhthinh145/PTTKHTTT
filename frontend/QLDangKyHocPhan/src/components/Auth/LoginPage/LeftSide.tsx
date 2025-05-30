import leftPic from "../../../assets/pic/loginPic.jpg";

export const LeftSide = () => {
  return (
    <div className="flex-1 flex items-cetner justify-center p-4">
      <img src={leftPic} alt="loginPic" className="max-w-full h-auto" />
    </div>
  );
};
