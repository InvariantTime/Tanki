import { AlertDialog, AlertDialogBody, AlertDialogContent, AlertDialogFooter, AlertDialogHeader, AlertDialogOverlay, Button, Input, Text } from "@chakra-ui/react";
import { FocusableElement } from "@chakra-ui/utils";
import { RefObject } from "react";

interface Props {
    onClose: () => void
    isOpen: boolean
    cancelRef: RefObject<HTMLButtonElement | FocusableElement>
}

export const RoomCreationForm = ({ onClose, isOpen, cancelRef }: Props) => {

    return (
        <AlertDialog
            motionPreset="slideInBottom"
            onClose={onClose}
            isOpen={isOpen}
            leastDestructiveRef={cancelRef}
            isCentered
        >

            <AlertDialogOverlay />

            <AlertDialogContent bg="transparent">
                <div className="bg-gray-800 border border-gray-700 rounded shadow-lg shadow-blue-950">
                    
                    <AlertDialogHeader fontSize={18} color="green">
                        <Text className="text-slate-200">
                            New room
                        </Text>
                    </AlertDialogHeader>

                    <AlertDialogBody>
                        <div className="mb-4">
                            <Text className="text-slate-400 mb-3">
                                input name of new room
                            </Text>

                            <Input placeholder="name" />
                        </div>
                    </AlertDialogBody>


                    <AlertDialogFooter>
                        <Button colorScheme="blue">Create</Button>
                        <Button colorScheme="red" ml={3} onClick={onClose}>Cancel</Button>
                    </AlertDialogFooter>
                </div>
            </AlertDialogContent>
        </AlertDialog>

    );
}