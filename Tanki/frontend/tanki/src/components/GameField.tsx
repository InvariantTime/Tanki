import { Text } from "@chakra-ui/react"
import { useEffect, useRef } from "react"
import { DrawTank } from "../services/Drawing";

interface Props {

}

export const GameField = () => {
    const ref = useRef<HTMLCanvasElement>(null);

    useEffect(() => {
        const context = ref.current?.getContext("2d");
        context?.clearRect(0, 0, 100, 100);


        if (context !== null)
            DrawTank(context!);
    });

    return (
        <div className="bg-white rounded border
            border-slate-300 shadow-xl h-full p-2">
            
            <Text as="b">Game</Text>

            <div className="h-full justify-center items-center flex">
                <canvas ref={ref} className="bg-slate-200" />
            </div>
        </div>
    )
}