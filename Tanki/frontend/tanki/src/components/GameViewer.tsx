import { AspectRatio } from "@chakra-ui/react";

export const GameViewer = () =>
{
    return (
        <div className="dark:bg-gray-800 p-5 shadow-lg shadow-blue-950 rounded border border-gray-700">

            <AspectRatio>
                <canvas/>
            </AspectRatio>
        </div>
    );
}