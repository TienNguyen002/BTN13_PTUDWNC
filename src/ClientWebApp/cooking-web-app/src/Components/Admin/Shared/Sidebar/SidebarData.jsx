import { icon } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faBook, faBowlFood, faPaste, faUser, faPaperPlane, faRightFromBracket } from '@fortawesome/free-solid-svg-icons'
import React from 'react'

export const SidebarData = [
    {
        title: "Khóa học",
        path: '/admin/courses',
        icon: <FontAwesomeIcon icon={faBook}/>,
        cName: "nav-text"
    },
    {
        title: "Công thức",
        path: '/admin/recipes',
        icon: <FontAwesomeIcon icon={faBowlFood}/>,
        cName: "nav-text"
    },
    {
        title: "Bài viết",
        path: '/admin/posts',
        icon: <FontAwesomeIcon icon={faPaste}/>,
        cName: "nav-text"
    },
    {
        title: "Tác giả",
        path: '/admin/authors',
        icon: <FontAwesomeIcon icon={faUser}/>,
        cName: "nav-text"
    },
    {
        title: "Chủ đề",
        path: '/admin/categories',
        icon: <FontAwesomeIcon icon={faPaperPlane}/>,
        cName: "nav-text"
    },
    {
        title: "Đầu bếp",
        path: '/admin/chefs',
        icon: <FontAwesomeIcon icon={faUser}/>,
        cName: "nav-text"
    },
    {
        title: "Học viên",
        path: '/admin/students',
        icon: <FontAwesomeIcon icon={faUser}/>,
        cName: "nav-text"
    },
    {
        title: "Đăng xuất",
        path: '/',
        icon: <FontAwesomeIcon icon={faRightFromBracket}/>,
        cName: "nav-text"
    }
]