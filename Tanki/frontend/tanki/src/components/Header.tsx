import { Image, Text } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import { BsArrowUpCircle } from "react-icons/bs";

interface Props {
    name: string,
    score: number
}

export const Header = ({ name, score }: Props) => {

    return (
        <div className="p-1 flex items-center justify-between w-full
            bg-white border-b border-slate-300 shadow-lg flex-wrap">
            <HeaderLogo />
            <nav>
                <ul className="flex gap-5 text-gray-400 ">
                    <li className="cursor-pointer hover:text-blue-600">How to play</li>
                    <li className="cursor-pointer hover:text-blue-600">Leaders</li>
                </ul>
            </nav>

            <div className="pr-4 flex flex-row gap-8">
                <div className="flex gap-2 text-center items-center">
                    <BsArrowUpCircle color="green" size="25"/>
                    {score}
                </div>
                <div>
                    <b>{name}</b>
                </div>
            </div>

        </div>
    );
}

const HeaderLogo = () => {
    const navigate = useNavigate();

    const onClick = () => {
        navigate("/");
    }

    return (
        <div className="items-center p-2 flex flex-row cursor-pointer" onClick={onClick}>
            <Image src={process.env.PUBLIC_URL + "/img/logo.png"}
                w="60px" mr="10px" />
            <Text as="b" fontSize={24}>
                Tanki
            </Text>
        </div>
    );
}