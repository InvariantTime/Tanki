import { Alert, AlertDescription, AlertIcon, AlertTitle, Button, Text, Textarea } from "@chakra-ui/react";
import { Editor } from "@monaco-editor/react";

export const SessionEditor = () =>
{
    return (
        <div className="bg-white rounded border p-4
            border-slate-300 shadow-xl h-full w-full">
                <Text as="b">Editor</Text>

                <div className="h-[80%]">
                    
                </div>

                <div className="border-t p-2 border-slate-400">
                    
                    <div className="mb-5">

                    <Alert status="error" visibility="visible">
                        <AlertIcon/>
                        <AlertTitle>Code error!</AlertTitle>
                        <AlertDescription></AlertDescription>
                    </Alert>
                    </div>

                    <Button float="right" colorScheme="green">Send</Button>
                </div>
        </div>
    );
}